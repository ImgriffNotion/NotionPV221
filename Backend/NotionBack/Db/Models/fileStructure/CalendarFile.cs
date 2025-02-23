using System;
using NotionBack.Db.Models.pageContents.pageInPageContents;

namespace NotionBack.Db.Models.fileStructure;

public class CalendarFile
{
    public Guid FileId { get; set; }
    public File File { get; set; }

    public Guid CalendarContentId { get; set; }
    public CalendarContent CalendarContent { get; set; }
}
