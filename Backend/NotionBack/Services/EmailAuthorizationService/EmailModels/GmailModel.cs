namespace NotionBack.Services.EmailAuthorizationService.EmailModels
{
    public class GmailModel : IEmailModel
    {
        public String email { get; set; }
        public String given_name { get; set; }
        public String family_name { get; set; }
        public String picture { get; set; }

        public string GetEmail()
        {
            return this.email;
        }

        public string GetFirstName()
        {
            return this.given_name;
        }

        public string GetLastName()
        {
            return this.family_name;
        }

        public string GetPhoto()
        {
            return this.picture;
        }
    }
}
