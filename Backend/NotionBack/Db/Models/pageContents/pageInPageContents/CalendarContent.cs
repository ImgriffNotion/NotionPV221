using System;
using NotionBack.Db.Models.fileStructure;

namespace NotionBack.Db.Models.pageContents.pageInPageContents;

public class CalendarContent
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PlanedDate { get; set; }
    public int Untitled { get; set; }
    public string Color { get; set; }

    public Guid CalendarId { get; set; }
    public Calendar Calendar { get; set; }

    public ICollection<CalendarFile> Files { get; set; } = [];  
}
