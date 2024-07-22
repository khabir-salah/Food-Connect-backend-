using Application.Interfaces;
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
        public UserRepository(FoodConnectDB context) : base(context)
        {
        }

        public async Task SavePasswordResetTokenAsync(User user, string token)
        {
            user.PasswordResetToken = token;
            user.PasswordExpireTime = DateTime.UtcNow.AddHours(1);
            Save();
        }
    }
}
