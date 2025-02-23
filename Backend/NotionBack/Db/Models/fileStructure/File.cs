using System;

namespace NotionBack.Db.Models.fileStructure;

public class File
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public ICollection<CalendarFile> CalendarFiles { get; set; } = [];
    public ICollection<ListFile> ListFiles { get; set; } = [];
}
