
using Microsoft.IdentityModel.Tokens;
using NotionBack.DAL.Interfaces;
using NotionBack.Services.RandomService;
using System.Text.RegularExpressions;

namespace NotionBack.Services.SlugService
{
    public class SlugerService(IUnitOfWork unitOfWork, IRandomService randomService) : ISlugerService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRandomService _randomService = randomService;

        public async Task<string> GenerateUniqueSlug(string text)
        {
            string baseSlug = this.Slugify(text);
            string slug = baseSlug;
            bool isUnique = false;

            if (slug.IsNullOrEmpty())
            {
                slug = _randomService.CreatorSymbolsByCount(10);
            }

            while (!isUnique)
            {
                try
                {
                    var page = await _unitOfWork.Pages.GetPageBySlug(slug);
                    
                    if (page != null)
                        slug = $"{baseSlug}-{_randomService.CreatorSymbolsByCount(6)}";
                }
                catch (Exception)
                {
                    isUnique = true; 
                }
            }

            return slug;
        }

        public string Slugify(string text)
        {
            text = text.ToLowerInvariant();
            text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
            text = Regex.Replace(text, @"\s+", " ").Trim();
            text = text.Replace(" ", "-");
            return text;
        }
    }
}
