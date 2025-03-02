using System;
using List = NotionBack.DAL.Models.pageContents;


namespace NotionBack.DAL.Models.pageContents;

public class Board
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime? DeleteDt { get; set; }

    public Guid ParentPageId { get; set; }
    public Page? ParentPage { get; set; }

    public ICollection<List> Lists { get; set; } = [];
}
