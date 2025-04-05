using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Models.ModelsDTO.ContentDTO
{
    public class GalleryDTO
    {
        public Guid Id { get; set; }
        public Guid ParentPageId { get; set; }
        public String? Title { get; set; }
        public List<GalleryContentDTO>? InternalContent { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeleteDt { get; set; }
    }
}
