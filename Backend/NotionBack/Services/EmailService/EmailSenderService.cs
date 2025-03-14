using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using MimeKit;
using NotionBack.Models;
using NotionBack.Services.RandomService;
using static System.Net.Mime.MediaTypeNames;


namespace NotionBack.Services.EmailService
{
    public class EmailSenderService() : IEmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _emailFrom = "imgriff365@gmail.com";
        private readonly string _emailPassword = "bdvi rzap wcrs pynb";
        private readonly string _htmlTemplatePath = "Services/EmailService/imgriff_emailPage.html";
        private readonly string _logoPath = "Services/EmailService/img/Frame 220 1.png";


        public async Task SendEmail(string toEmail, string verificationCode)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Imgriff Security", _emailFrom));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = "Your Imgriff Login Verification Code";

            var bodyBuilder = new BodyBuilder();


            if (File.Exists(_htmlTemplatePath))
            {
                bodyBuilder.HtmlBody = File.ReadAllText(_htmlTemplatePath)
                    .Replace("{VerificationCode}", verificationCode)
                    .Replace("{ExpirationTime}", DateTime.UtcNow.AddMinutes(10).ToString("MMMM dd, yyyy HH:mm UTC"));
            }
            else
            {
                bodyBuilder.HtmlBody = "<p>Failed to load email template.</p>";
            }

            if (File.Exists(_logoPath))
            {
                var logo = bodyBuilder.LinkedResources.Add(_logoPath);
                logo.ContentId = "ImgriffLogo";
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailFrom, _emailPassword);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);

        }
    }
}
