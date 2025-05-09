using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO
{
    public class PageDTO
    {
        [JsonPropertyName("id")] 
        public Guid Id { get; set; }
        
        [Required]
        [JsonPropertyName("ownerId")]
        public Guid OwnerId { get; set; }

        [Required]
        [JsonPropertyName("title")] 
        public String? Title { get; set; }
        
        [Required]
        [JsonPropertyName("banner")] 
        public String? Banner {  get; set; }
        
        [Required]
        [JsonPropertyName("icon")] 
        public String? Icon { get; set; }
        
        [JsonPropertyName("slug")] 
        public String? Slug { get; set; }
        
        [Required]
        [JsonPropertyName("type")] 
        public String? Type { get; set; }
        
        [JsonPropertyName("createdAt")] 
        public DateTime? CreatedAt { get; set; }
        
        [JsonPropertyName("deleteDt")] 
        public DateTime? DeleteDt { get; set; }
        
        [JsonPropertyName("content")]
        public Object? Content { get; set; }
    }
}
