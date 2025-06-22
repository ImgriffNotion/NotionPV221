using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO
{
    public class PageDTO
    {
        [JsonPropertyName("id")]
        
        public Guid Id { get; set; }
        
        [JsonPropertyName("ownerId")]
         
        public Guid? OwnerId { get; set; }

        [JsonPropertyName("title")] 
        public String? Title { get; set; }
        
        [JsonPropertyName("banner")] 
        public String? Banner {  get; set; }
        
        [JsonPropertyName("icon")] 
        public String? Icon { get; set; }

        
        [JsonPropertyName("slug")] 
        public String? Slug { get; set; }
        
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
