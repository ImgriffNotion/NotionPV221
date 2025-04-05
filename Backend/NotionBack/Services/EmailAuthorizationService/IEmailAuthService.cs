using NotionBack.Services.EmailAuthorizationService.EmailModels;

namespace NotionBack.Services.EmailAuthorizationService
{
    public interface IEmailAuthService
    {
        public IEmailModel GetEmailModelByJson(String json);
        public IEmailModel GetEmailModelByResponse(Object tmpModel);
    }
}
