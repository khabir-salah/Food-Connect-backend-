using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Command.Update
{
    public class ManagerProfile
    {
        //updating manager profile
        public class Handler : IRequestHandler<ManagerUpdateCommandModel, BaseResponse<string>>
        {
            private readonly IUserRepository _userRepo;
            private readonly ICurrentUser _currentUser;
            private readonly IManagerRepository _managerRepo;
            public Handler(IUserRepository userRepo, ICurrentUser currentUser, IManagerRepository managerRepo) 
            { 
                _userRepo = userRepo;
                _currentUser = currentUser;
                _managerRepo = managerRepo;
            }
            public async Task<BaseResponse<string>> Handle(ManagerUpdateCommandModel request, CancellationToken cancellationToken)
            {
                var loggedinUser = await _currentUser.LoggedInUser();

                //getting user to update property
                var getUser = await _userRepo.GetUserAsync(u => u.Id == loggedinUser.Id);

                if (getUser == null)
                {
                    return new BaseResponse<string>
                    {
                        IsSuccessfull = false,
                        Message = "Family Head not FOUND"
                    };
                }
                var manager = await _managerRepo.Get(i => i.Id == loggedinUser.Id);
                //updating user
                getUser.ProfileImage = getUser.ProfileImage ?? request.ProfileImage;
                getUser.PhoneNumber = getUser.PhoneNumber ?? request.PhoneNumber;
                getUser.Name = getUser.Name;
                getUser.Email = getUser.Email;
                getUser.IsEmailConfirmed = getUser.IsEmailConfirmed ? true : false;
                getUser.Address = getUser.Address ?? request.Address;

                
                manager.Nin = manager.Nin;
                manager.LOcalGovernment = request.LOcalGovernment;
                manager.PostalCode = request.PostalCode;
                manager.City = request.City;

                _userRepo.Update(getUser);
                _managerRepo.Update(manager);
                _currentUser.SaveUserAsync();

                return new BaseResponse<string>
                {
                    IsSuccessfull = true,
                    Message = "Profile updated Successfully"
                };
            }
        }
    }
}
