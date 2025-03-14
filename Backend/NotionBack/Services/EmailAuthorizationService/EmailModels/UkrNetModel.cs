namespace NotionBack.Services.EmailAuthorizationService.EmailModels
{
    public class UkrNetModel : IEmailModel
    {
        public String email { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String profile_picture { get; set; }

        public string GetEmail()
        {
            return this.email;
        }

        public string GetFirstName()
        {
            return this.first_name;
        }

        public string GetLastName()
        {
            return this.last_name;
        }

        public string GetPhoto()
        {
            return this.profile_picture;
        }
    }
}
