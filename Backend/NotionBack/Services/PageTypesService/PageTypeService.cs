using NotionBack.Models.Enums;

namespace NotionBack.Services.PageTypesService
{
    public class PageTypeService : IPageTypeService
    {
        public int GetCodeOfPageType(string pageType)
        {
            foreach (var type in Enum.GetValues(typeof(PageType)))
            {
                if(type.ToString() == pageType)
                    return (int)type;
            }
            return -1;
        }
    }
}
