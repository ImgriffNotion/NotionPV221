using System.Text.Json.Serialization;

namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class TableContentDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("tableId")]
        public Guid TableId { get; set; }
        [JsonPropertyName("data")]
        public String? Data { get; set; }
        [JsonPropertyName("foreground")]
        public String? Foreground { get; set; }
        [JsonPropertyName("background")]
        public String? Background { get; set; }
        [JsonPropertyName("row")]
        public int Row { get; set; }
        [JsonPropertyName("col")]
        public int Col { get; set; }

    }
}
