using NotionBack.Models.ModelsDTO.ContentDTO;

namespace NotionBack.Models.ModelsDTO
{
    public class PageDTO<T>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public String? Title { get; set; }
        public String? Banner {  get; set; }
        public String? Icon { get; set; }
        public String? Slug { get; set; }
        public String? Type { get; set; }
        public DateTime? DeleteDt { get; set; }
        public T? Content { get; set; }
    }
}
