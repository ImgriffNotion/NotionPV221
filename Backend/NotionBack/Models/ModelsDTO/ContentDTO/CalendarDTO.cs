using Microsoft.AspNetCore.Mvc;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;
using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO.ContentDTO
{
    public class CalendarDTO
    {
        
        [JsonPropertyName("id")] 
        public Guid? Id { get; set; }
        
        [JsonPropertyName("parentPageId")] 
        public Guid? ParentPageId { get; set; }
        
        [JsonPropertyName("title")] 
        public String? Title { get; set; }
        
        [JsonPropertyName("internalContent")] 
        public List<CalendarContentDTO>? InternalContent { get; set; }
        
        [JsonPropertyName("createdAt")] 
        public DateTime? CreatedAt { get; set; }
        
        [JsonPropertyName("deleteDt")] 
        public DateTime? DeleteDt { get; set; }
    }
}
