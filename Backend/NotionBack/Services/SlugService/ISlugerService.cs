namespace NotionBack.Services.SlugService
{
    public interface ISlugerService
    {
        public Task<String> GenerateUniqueSlug(String title);
        public String Slugify(String title);
    }
}
