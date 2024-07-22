using Domain.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class ResetPasswordService
    {
        public string GeneratePasswordResetToken(User user)
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        

        


        public class ForgotPasswordRequest
        {
            public string Email { get; set; }
        }

        public class ResetPasswordRequest
        {
            public string Token { get; set; }
            public string Password { get; set; }
        }




    }
}
