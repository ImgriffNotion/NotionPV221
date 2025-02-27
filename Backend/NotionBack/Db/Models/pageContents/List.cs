using System;
using NotionBack.Db.Models.pageContents.pageInPageContents;

namespace NotionBack.Db.Models.pageContents;

public class List
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime? DeleteDt { get; set; }

    public Guid ParentPageId { get; set; }
    public Page ParentPage { get; set; }

    public Guid BoardId { get; set; }
    public Board Board { get; set; }

    public ICollection<ListContent> Contents { get; set; } = new List<ListContent>();
}
