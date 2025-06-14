namespace NotionBack.DAL.Models.Templates.pageContents.pageInPageContents
{
    public class GalleryContentTemplate
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public DateTime? Date { get; set; }

        public Guid? GalleryTemplateId { get; set; }
        public GalleryTemplate? GalleryTemplate { get; set; }
    }
}
