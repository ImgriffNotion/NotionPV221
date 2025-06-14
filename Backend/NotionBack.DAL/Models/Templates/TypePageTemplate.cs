namespace NotionBack.DAL.Models.Templates
{
    public class TypePageTemplate
    {
        public Guid Id { get; set; } = new Guid();
        public string? Name { get; set; }
        public int TypeCode { get; set; }
        public ICollection<Template> Templates { get; set; } = [];
    }
}
