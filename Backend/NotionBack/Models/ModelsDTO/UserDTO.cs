using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO
{
    public class UserDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public String? Name { get; set; }
        [JsonPropertyName("lastname")]
        public String? Lastname { get; set; }
        [JsonPropertyName("email")]
        public String? Email { get; set; }
        [JsonPropertyName("avatar")]
        public String? Avatar { get; set; }

    }
}
