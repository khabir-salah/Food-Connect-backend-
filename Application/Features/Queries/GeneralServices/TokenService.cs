using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GeneralServices
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        public TokenService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> GenerateEmailConfirmationToken(Guid Id)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                var user = await _userRepository.Get(u => u.Id == Id);
                var token = user.PasswordResetToken = Convert.ToBase64String(tokenData);
                _userRepository.Update(user);
                _userRepository.Save();
                return token;
            }
        }

        

        public string GeneratePasswordResetToken(User user)
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        //public async Task StoreTokenAsync(string userId, string token)
        //{
        //    var user = await _dbContext.Users.FindAsync(userId);
        //    if (user != null)
        //    {
        //        user.EmailConfirmationToken = token;
        //        user.EmailConfirmationTokenExpiration = DateTime.UtcNow.AddHours(24); // Token valid for 24 hours
        //        await _dbContext.SaveChangesAsync();
        //    }
        //}

        public async Task<bool> ValidateEmailTokenAsync(Guid userId, string token)
        {
            var user = await _userRepository.Get(u => u.Id == userId && u.PasswordResetToken == token);

            if (user == null || user.PasswordExpireTime < DateTime.UtcNow)
            {
                return false;
            }

            // Activate the user's account
            user.IsEmailConfirmed = true;
            user.IsActivated = true;
            user.PasswordResetToken = null; 
            user.PasswordExpireTime = null; 
            _userRepository.Update(user);
            _userRepository.Save();

            return true;
        }



    }
}
