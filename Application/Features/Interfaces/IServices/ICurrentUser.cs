using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IServices
{
    public interface ICurrentUser
    {
        Task<User> LoggedInUser();
        Task<User> GetUserAsync(Guid Id);
        Task SavePasswordResetTokenAsync(User user, string token);
        void SaveUserAsync();
        Task<User> GetUserTokenAsync(string Id);
        Task<User> GetUserEmailAsync(string email);

        Task<bool> DeactivateUser(Guid id);
        Task<bool> ActivateUser(Guid id);
    }
}
