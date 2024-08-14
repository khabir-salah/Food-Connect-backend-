using MimeKit;

using Application.Features.Interfaces.IServices;


namespace Application.Features.Queries.GeneralServices
{
    public class EmailService : IEmailService
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
            await client.ConnectAsync("sandbox.smtp.mailtrap.io", 465, false);
            await client.AuthenticateAsync("c3a70fdd62b5ea", "fb2c6b9b9faefe");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }


        public async Task SendPasswordResetEmailAsync(string email, string callbackUrl)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("FoodConnect", "no-reply@FoodConnect.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Reset Password";
            message.Body = new TextPart("plain")
            {
                Text = $"Please reset your password by clicking here: {callbackUrl}"
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync("smtp.mailtrap.io", 587, false);
            await client.AuthenticateAsync("c3a70fdd62b5ea", "fb2c6b9b9faefe");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

       
    }
}
