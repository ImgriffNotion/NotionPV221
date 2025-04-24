using NotionBack.Models.ModelsDTO.ContentDTO;

namespace NotionBack.Models.ModelsDTO
{
    public class PageDTO
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public String? Title { get; set; }
        public String? Banner {  get; set; }
        public String? Icon { get; set; }
        public String? Slug { get; set; }
        public PageTypeDTO? Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeleteDt { get; set; }
        public Object? Content { get; set; }
    }
}
