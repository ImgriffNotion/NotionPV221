using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class ListContentDTO
    {
        
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }
        
        [JsonPropertyName("listId")]
        public Guid? ListId { get; set; }
        
        [JsonPropertyName("title")]
        public String? Title { get; set; }
        
        [JsonPropertyName("number")]
        public String? Number { get; set; }
        
        [JsonPropertyName("description")]
        public String? Description { get; set; }
        
        [JsonPropertyName("color")]
        public String? Color { get; set; }
        
        [JsonPropertyName("index")]
        public int Index { get; set; }
        
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }
        
        [JsonPropertyName("files")]
        public List<FileDTO> Files { get; set; }
    }
}
