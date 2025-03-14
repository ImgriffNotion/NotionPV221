using System;
using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Models.fileStructure;

public class ListFile
{
    public Guid FileId { get; set; }
    public File File { get; set; }

    public Guid ListContentId { get; set; }
    public ListContent ListContent { get; set; }
}
