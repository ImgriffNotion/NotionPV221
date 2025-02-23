using System;
using NotionBack.Db.Models.pageContents.pageInPageContents;

namespace NotionBack.Db.Models.fileStructure;

public class ListFile
{
    public Guid FileId { get; set; }
    public File File { get; set; }

    public Guid ListContentId { get; set; }
    public ListContent ListContent { get; set; }
}
