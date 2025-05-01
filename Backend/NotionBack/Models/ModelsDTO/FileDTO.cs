using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO
{
    public class FileDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")] 
        public String? Name { get; set; }
        [JsonPropertyName("fileUrl")] 
        public String? Url { get; set; }

    }
}
