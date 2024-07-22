using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using MailKit.Net.Smtp;


namespace Application.Queries
{
    public class EmailService
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

                using var client = new MailKit.Net.Smtp.SmtpClient();
                await client.ConnectAsync("smtp.mailtrap.io", 587, false);
                await client.AuthenticateAsync("username", "password");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }


        public async Task SendPasswordResetEmailAsync(string email, string callbackUrl)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("AppName", "no-reply@appname.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Reset Password";
            message.Body = new TextPart("plain")
            {
                Text = $"Please reset your password by clicking here: {callbackUrl}"
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync("smtp.mailtrap.io", 587, false);
            await client.AuthenticateAsync("username", "password");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
