namespace NotionBack.DAL.Models
{
    public class TypePage
    {
        public Guid Id { get; set; } = new Guid();
        public string? Name { get; set; }
        public int TypeCode { get; set; }
        public ICollection<Page> Pages { get; set; } = [];
    }
}
