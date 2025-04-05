using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Models.ModelsDTO.ContentDTO
{
    public class CalendarDTO
    {
        public Guid Id { get; set; }
        public Guid ParentPageId { get; set; }
        public String? Title { get; set; }
        public List<CalendarContentDTO>? InternalContent { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeleteDt { get; set; }
    }
}
