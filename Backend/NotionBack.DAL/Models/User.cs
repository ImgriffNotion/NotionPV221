namespace NotionBack.DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }

        public ICollection<Page> Pages { get; set; } = new List<Page>();
    }
}
