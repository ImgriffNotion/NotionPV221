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
        private readonly string _emailPassword = "xsnq tufr ukqf wdnz";
        private readonly string _htmlTemplatePath = "Services/EmailService/imgriff_emailPage.html";
        private readonly string _logoPath = "Services/EmailService/img/Frame 220 1.png";


        public async Task SendEmail(string toEmail, string verificationCode)
        {

            Console.WriteLine($"\n\n\n{verificationCode}\n\n\n");
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
                bodyBuilder.HtmlBody = $@"<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}

        .container {{
            max-width: 600px;
            margin: 20px auto;
            background: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }}

        .header {{
            text-align: center;
            padding: 10px;
        }}

        .logo {{
            width: 150px;
        }}

        .content {{
            padding: 20px;
            font-size: 16px;
            color: #333;
        }}

        .verification-code {{
            font-size: 24px;
            font-weight: bold;
            background: #f8f9fa;
            padding: 15px;
            border-radius: 5px;
            text-align: center;
            margin: 20px 0;
        }}

        .footer {{
            text-align: center;
            padding: 10px;
            font-size: 12px;
            color: #888;
        }}

    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <img src=""cid:ImgriffLogo"" alt=""Imgriff Logo"" width=""150"">
        </div>
        <div class=""content"">
            <p>Someone is trying to log in to your Imgriff account.</p>

            <p>Please enter the following code to complete the login:</p>
            <div class=""verification-code"">{verificationCode}</div>

            <p><i>Please note that this code will expire on {DateTime.UtcNow.AddMinutes(10).ToString("MMMM dd, yyyy HH:mm UTC")}.</i></p>

            <p>If you did NOT initiate this login, please skip this message.</p>

            <div class=""footer"">
                <p>&copy; 2025 Imgriff, All Rights Reserved.</p>
            </div>
        </div>
    </div>
</body>
</html>";
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
