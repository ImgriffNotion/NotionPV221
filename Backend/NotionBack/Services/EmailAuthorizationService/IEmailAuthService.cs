using NotionBack.Services.EmailAuthorizationService.EmailModels;

namespace NotionBack.Services.EmailAuthorizationService
{
    public interface IEmailAuthService
    {
        public IEmailModel GetModelByEmail(String json); 
    }
}
