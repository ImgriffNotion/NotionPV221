using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Models.pageContents;

public class Calendar
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeleteDt { get; set; }

    public Guid? ParentPageId { get; set; }
    public Page? ParentPage { get; set; }

    public ICollection<CalendarContent> Contents { get; set; } = [];
}
