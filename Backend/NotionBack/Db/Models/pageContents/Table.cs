using NotionBack.Db.Models.pageContents.pageInPageContents;

namespace NotionBack.Db.Models.pageContents
{
    public class Table
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public DateTime? DeleteDt { get; set; }

        public Guid ParentPageId { get; set; }
        public Page ParentPage { get; set; }

        public ICollection<TableContent> Contents { get; set; } = new List<TableContent>();
    }
}