namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class GalleryContentDTO
    {
        public Guid Id { get; set; }
        public Guid GalleryId { get; set; }
        public String? Title { get; set; }
        public String? Url { get; set; }
    }
}
