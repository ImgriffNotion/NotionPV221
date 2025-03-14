using NotionBack.Services.EmailAuthorizationService.EmailModels;

namespace NotionBack.Services.EmailAuthorizationService
{
    public class EmailAuthService : IEmailAuthService
    {
        public IEmailModel GetModelByEmail(string json)
        {
            switch (json) {
                case "gmail.com":
                    return new GmailModel();
                case "icloud.com":
                    return new ICloudModel();
                case "urk.net": 
                    return new UkrNetModel();
                case "mail.ru":
                    return new MailModel();
                default:
                    return null;
            }
        }
    }
}
