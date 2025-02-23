using System;
using NotionBack.Db.Models.fileStructure;

namespace NotionBack.Db.Models.pageContents.pageInPageContents;

public class ListContent
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string Number { get; set; }
    public DateTime? Date { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string Label { get; set; }
    public int Index { get; set; }

    public Guid ListId { get; set; }
    public List List { get; set; }

    public ICollection<ListFile> Files { get; set; } = new List<ListFile>();
}
