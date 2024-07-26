using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using Infrastructure.persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            return await _foodConnectDB.User.Include(r => r.Role).FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var check = await GetUserAsync(u => u.Email == email);
            return check != null ? true : false;
        }

        

    }
}
