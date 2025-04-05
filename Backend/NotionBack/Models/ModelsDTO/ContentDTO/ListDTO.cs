using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Models.ModelsDTO.ContentDTO
{
    public class ListDTO
    {
        public Guid Id { get; set; }
        public Guid ParentPageId { get; set; }
        public Guid BoardId { set; get; } 
        public String? Title { get; set; }
        public List<ListContentDTO>? InternalContent { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeleteDt { get; set; }
    }
}
