namespace NotionBack.Db.Models.pageContents.pageInPageContents
{
    public class TableContent
    {
        public Guid Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Data { get; set; }
        public string Foreground { get; set; }
        public string Background { get; set; }

        public Guid TableId { get; set; }
        public Table Table { get; set; }
    }
}