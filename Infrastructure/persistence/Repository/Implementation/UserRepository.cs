using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using Infrastructure.persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.persistence.Repository.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly FoodConnectDB _foodConnectDB;
        public UserRepository(FoodConnectDB context) : base(context)
        {
            _foodConnectDB = context;
        }

        public async Task SavePasswordResetTokenAsync(User user, string token)
        {
            user.PasswordResetToken = token;
            user.PasswordExpireTime = DateTime.UtcNow.AddHours(1);
            Save();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _foodConnectDB.User.Include(r => r.Role).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var check = await GetUserByEmailAsync(email);
            return check != null ? true : false;
        }
    }
}
