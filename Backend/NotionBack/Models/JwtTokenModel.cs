using NotionBack.Models.ModelsDTO;

namespace NotionBack.Models
{
    public class JwtTokenModel
    {
        public String Jwt {  get; set; }
        public UserDTO User { get; set; }
    }
}
