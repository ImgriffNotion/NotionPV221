using NotionBack.Db.Models.pageContents.pageInPageContents;

namespace NotionBack.Db.Models.pageContents
{
    public class Gallery
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? DeleteDt { get; set; }

        public Guid ParentPageId { get; set; }
        public Page ParentPage { get; set; }

        public ICollection<GalleryContent> Contents { get; set; } = new List<GalleryContent>();
    }
}