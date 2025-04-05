namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class EmptyPageContentDTO
    {
        public Guid Id { get; set; }
        public Guid ParentPageId { get; set; }
        public string? Text { get; set; }
        public int Index { get; set; }

    }
}
