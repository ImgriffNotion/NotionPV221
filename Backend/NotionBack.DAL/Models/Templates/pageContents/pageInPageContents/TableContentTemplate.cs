namespace NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

public class TableContentTemplate
{
    public Guid Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public string? Data { get; set; }
    public string? Foreground { get; set; }
    public string? Background { get; set; }

    public Guid? TableTemplateId { get; set; }
    public TableTemplate? TableTemplate { get; set; }
}