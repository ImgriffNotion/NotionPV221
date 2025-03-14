namespace NotionBack.Services.EmailService
{
    public interface IEmailService
    {
        public Task SendEmail(string toEmail, string verificationCode);
    }
}
