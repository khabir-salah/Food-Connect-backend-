using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IRepositries
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task SavePasswordResetTokenAsync(User user, string token);
        Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate);
        Task<bool> IsEmailExist(string email);
    }
}
