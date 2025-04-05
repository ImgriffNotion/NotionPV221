namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class ListContentDTO
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public String? Title { get; set; }
        public String? Text { get; set; } //убрать
        public String? Number { get; set; } 
        public String? Status { get; set; } // убрать
        public String? Description { get; set; }
        public String? Label { get; set; }
        public int Index { get; set; }
        public DateTime? Date { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
