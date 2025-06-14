using NotionBack.DAL.Models.fileStructure;

namespace NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

public class ListContentTemplate
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public string? Number { get; set; }
    public DateTime? Date { get; set; }
    public string? Status { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public int Index { get; set; }

    public Guid? ListTemplateId { get; set; }
    public ListTemplate? ListTemplate { get; set; }
}
