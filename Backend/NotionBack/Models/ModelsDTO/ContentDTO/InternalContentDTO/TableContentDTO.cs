namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class TableContentDTO
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public String? Data { get; set; }
        public String? Foreground { get; set; }
        public String? Background { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

    }
}
