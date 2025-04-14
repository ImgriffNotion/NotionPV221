namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class ListContentDTO
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public String? Title { get; set; }
        public String? Number { get; set; } 
        public String? Description { get; set; }
        public String? Color { get; set; }
        public int Index { get; set; }
        public DateTime? Date { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
