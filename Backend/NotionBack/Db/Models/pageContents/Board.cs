using System;

namespace NotionBack.Db.Models.pageContents;

public class Board
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime? DeleteDt { get; set; }

    public Guid ParentPageId { get; set; }
    public Page ParentPage { get; set; }

    public ICollection<List> Lists { get; set; } = new List<List>();
}
