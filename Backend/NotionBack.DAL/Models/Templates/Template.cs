using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Models.Templates
{
    public class Template
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public Guid? TypeTemplateId { get; set; }
        public TypePageTemplate? TypeTemplate { get; set; }

        public ICollection<JustPageContentTemplate> JustPageContentTemplates { get; set; } = [];
        public ICollection<GalleryTemplate> GalleryTemplates { get; set; } = [];
        public ICollection<TableTemplate> TableTemplates { get; set; } = [];
        public ICollection<CalendarTemplate> CalendarTemplates { get; set; } = [];
        public ICollection<BoardTemplate> BoardTemplates { get; set; } = [];
        public ICollection<ListTemplate> ListTemplates { get; set; } = [];
    }
}
