using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class GalleryContentDTO
    {
        
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }
        
        [JsonPropertyName("galleryId")]
        public Guid? GalleryId { get; set; }
        
        [JsonPropertyName("title")]
        public String? Title { get; set; }
        
        [JsonPropertyName("url")]
        public FileDTO? file { get; set; }
        
        [JsonPropertyName("description")]
        public String? Description { get; set; }
        
        [JsonPropertyName("number")]
        public String? Number { get; set; }
        
        [JsonPropertyName("color")]
        public String? Color { get; set; }
        
        [JsonPropertyName("date")]
        public DateTime? Date {  get; set; }
    }
}
