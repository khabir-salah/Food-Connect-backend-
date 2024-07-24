using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string email, string callbackUrl);
        Task SendPasswordResetEmailAsync(string email, string callbackUrl);

    }
}
