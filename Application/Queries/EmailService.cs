using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class EmailService
    {
        public class Handler
        {
            public async Task SendEmailConfirmationAsync(string email, string callbackUrl)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("FoodConnect", "no-reply@FoodConnect.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Confirm your email";
                message.Body = new TextPart("plain")
                {
                    Text = $"Please confirm your account by clicking this link: {callbackUrl}"
                };

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.mailtrap.io", 587, false);
                await client.AuthenticateAsync("username", "password");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }


       
    }
}
