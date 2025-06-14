namespace NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

public class CalendarContentTemplate
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? PlanedDate { get; set; }
    public string? Untitled { get; set; }
    public string? Color { get; set; }

    public Guid? CalendarTemplateId { get; set; }
    public CalendarTemplate? CalendarTemplate { get; set; }
}
