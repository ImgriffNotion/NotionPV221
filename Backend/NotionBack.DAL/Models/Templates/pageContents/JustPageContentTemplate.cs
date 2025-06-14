namespace NotionBack.DAL.Models.Templates.pageContents
{
    public class JustPageContentTemplate
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }

        public Guid? TemplateId { get; set; }
        public Template? Template { get; set; }
    }
}
