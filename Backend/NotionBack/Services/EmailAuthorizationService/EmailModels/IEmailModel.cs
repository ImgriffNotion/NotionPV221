namespace NotionBack.Services.EmailAuthorizationService.EmailModels
{
    public interface IEmailModel
    {
        public String GetEmail();
        public String GetFirstName();
        public String GetLastName();
        public String GetPhoto();
    }
}
