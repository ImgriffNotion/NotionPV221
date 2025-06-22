using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO
{
    public class PageTypeDTO
    {
        
        [JsonPropertyName("id")] 
        public Guid Id { get; set; }
        
        [JsonPropertyName("name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("typeCode")]
        public int TypeCode { get; set; }
    }
}
