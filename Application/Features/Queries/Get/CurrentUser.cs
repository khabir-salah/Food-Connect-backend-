using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Get
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserRepository _userRepo;
        public CurrentUser(IHttpContextAccessor accessor, IUserRepository userRepo) 
        {
            _accessor = accessor;
            _userRepo = userRepo;
        }

        public async Task<User> GetUserAsync(Guid Id)
        {
            return await _userRepo.Get(u => u.Id == Id);
        }

        public async Task<User> GetUserTokenAsync(string Id)
        {
            return await _userRepo.Get(u => u.PasswordResetToken == Id);
        }


        public async Task<User> GetUserEmailAsync(string email)
        {
            return await _userRepo.Get(u => u.Email == email);
        }


        public void SaveUserAsync()
        {
            _userRepo.Save();
        }

        public async Task SavePasswordResetTokenAsync(User user, string token)
        {
             await _userRepo.SavePasswordResetTokenAsync(user, token);
        }


        public async Task<User> LoggedInUser()
        {
           try
            {
                var userId = _accessor?.HttpContext?.User?.Claims?.FirstOrDefault(u => u.Type == "Id").Value;
                var getUser = await _userRepo.GetUserAsync(u => u.Id.ToString() == userId);
                return getUser;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ValidateUserAsync(Guid id)
        {
            var user = await _userRepo.GetUserAsync(u => u.Id ==  id);
           
            if(!user.IsActivated)
            {
                user.IsActivated = true;
                _userRepo.Update(user);
            }
            else
            {
                user.IsActivated = false;
                _userRepo.Update(user);
            }
            _userRepo.Save();
           
            return user.IsActivated? true : false;
        }

       
    }
}
