using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Models.Templates.pageContents;

public class ListTemplate
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime? DeleteDt { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public Guid? TemplateId { get; set; }
    public Template? Template { get; set; }

    public Guid? BoardTemplateId { get; set; }
    public BoardTemplate? BoardTemplate { get; set; }

    public ICollection<ListContentTemplate> Contents { get; set; } = [];
}
