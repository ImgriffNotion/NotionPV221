using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Models
{
    public class Page
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Banner { get; set; }
        public string? Icon { get; set; }
        public DateTime? DeleteDt { get; set; }
        public string? Slug { get; set; }

        public Guid? TypeId { get; set; }
        public TypePage? Type { get; set; }

        public Guid? OwnerId { get; set; }
        public User? Owner { get; set; }
        public ICollection<JustPageContent> JustPageContents { get; set; } = [];
        public ICollection<Gallery> Galleries { get; set; } = [];
        public ICollection<Table> Tables { get; set; } = [];
        public ICollection<Calendar> Calendars { get; set; } = [];
        public ICollection<Board> Boards { get; set; } = [];
        public ICollection<List> Lists { get; set; } = [];
    }
}
