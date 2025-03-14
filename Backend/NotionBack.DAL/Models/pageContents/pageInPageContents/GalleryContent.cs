namespace NotionBack.DAL.Models.pageContents.pageInPageContents
{
    public class GalleryContent
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }

        public Guid GalleryId { get; set; }
        public Gallery? Gallery { get; set; }
    }
}
