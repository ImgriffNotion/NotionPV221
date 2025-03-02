using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Models.pageContents;

public class Calendar
{
    public Guid Id { get; set; }
    public string? Title { get; set; }

    public Guid ParentPageId { get; set; }
    public Page? ParentPage { get; set; }

    public ICollection<CalendarContent> Contents { get; set; } = [];
}
