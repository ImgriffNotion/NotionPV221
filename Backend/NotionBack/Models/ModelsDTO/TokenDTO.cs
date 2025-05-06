using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO
{
    public class TokenDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }
        [JsonPropertyName("iat")]
        public DateTime Iat { get; set; }
        [JsonPropertyName("exp")]
        public DateTime Exp {  get; set; }

        [JsonPropertyName("deleteDt")]
        public DateTime DeleteDt { get; set; }
        [JsonPropertyName("user")]
        public UserDTO User { get; set; }

    }
}
