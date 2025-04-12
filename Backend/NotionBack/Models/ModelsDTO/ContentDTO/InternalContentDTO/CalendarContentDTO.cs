namespace NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO
{
    public class CalendarContentDTO
    {
        public Guid Id { get; set; }
        public Guid CalendarId { get; set; }
        public String? Title { get; set; }
        public String? Description { get; set; }
        public String? Color { get; set; }
        public String? Number { get; set; }
        public DateTime? PlanedDate { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
