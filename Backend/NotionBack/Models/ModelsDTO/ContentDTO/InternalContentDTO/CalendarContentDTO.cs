using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class CalendarContentDTO
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }
        [JsonPropertyName("calendarId")]
        public Guid? CalendarId { get; set; }
        [JsonPropertyName("title")]
        public String? Title { get; set; }
        [JsonPropertyName("description")]
        public String? Description { get; set; }
        [JsonPropertyName("color")]
        public String? Color { get; set; }
        [JsonPropertyName("number")]
        public String? Number { get; set; }
        [JsonPropertyName("planedDate")]
        public DateTime? PlanedDate { get; set; }
        [JsonPropertyName("files")]
        public List<FileDTO> Files { get; set; }
    }
}
