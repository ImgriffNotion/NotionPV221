using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Models.fileStructure;

public class CalendarFile
{
    public Guid FileId { get; set; }
    public File File { get; set; }

    public Guid CalendarContentId { get; set; }
    public CalendarContent CalendarContent { get; set; }
}
