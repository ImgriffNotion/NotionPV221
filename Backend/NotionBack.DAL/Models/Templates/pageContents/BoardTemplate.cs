namespace NotionBack.DAL.Models.Templates.pageContents;

public class BoardTemplate
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeleteDt { get; set; }

    public Guid? TemplateId { get; set; }
    public Template? Template { get; set; }

    public ICollection<ListTemplate> ListTemplates { get; set; } = [];
}
