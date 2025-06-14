using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Models.Templates.pageContents
{
    public class GalleryTemplate
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeleteDt { get; set; }
        public Guid? TemplateId { get; set; }
        public Template? Template { get; set; }

        public ICollection<GalleryContentTemplate> Contents { get; set; } = [];
    }
}
