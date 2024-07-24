using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IServices
{
    public interface ITokenService
    {
        Task<string> GenerateEmailConfirmationToken(Guid Id);

        string GeneratePasswordResetToken(User user);
        Task<bool> ValidateEmailTokenAsync(Guid userId, string token);

    }
}
