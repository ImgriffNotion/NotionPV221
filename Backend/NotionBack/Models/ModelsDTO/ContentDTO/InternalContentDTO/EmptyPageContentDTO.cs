using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class EmptyPageContentDTO
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }
        [JsonPropertyName("parentPageId")]
        public Guid? ParentPageId { get; set; }
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
