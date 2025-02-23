namespace NotionBack.Db.Models.pageContents
{
    public class JustPageContent
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }

        public Guid ParentPageId { get; set; }
        public Page ParentPage { get; set; }
    }
}
