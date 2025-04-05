using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Models.ModelsDTO.ContentDTO
{
    public class TableDTO
    {
        public Guid Id { get; set; }
        public Guid ParentPageId { get; set; }
        public String? Title { get; set; }
        public int Rows {  get; set; }
        public int Columns { get; set; }
        public List<TableContentDTO>? InternalContent { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeleteDt { get; set; }
    }
}
