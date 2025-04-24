using NotionBack.DAL.Models;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;
using NotionBack.Services.ConverterService.UntypeContentService;

namespace NotionBack.Services.ConverterService.Page
{
    public class PageConverter(IContentConverterRegistry registry, IConvertService<PageTypeDTO, TypePage> typeConvertService) : IConvertService<PageDTO, NotionBack.DAL.Models.Page>
    {
        private readonly IContentConverterRegistry _contentRegistry = registry;
        private readonly IConvertService<PageTypeDTO, TypePage> _typeConvertService = typeConvertService;

        public DAL.Models.Page FromDTO(PageDTO model)
        {
            var page = new DAL.Models.Page()
            {
                Id = model.Id,
                Banner = model.Banner,
                Icon = model.Icon,
                OwnerId = model.OwnerId,
                Title = model.Title,
                CreatedAt = model.CreatedAt,
                DeleteDt = model.DeleteDt,
                Slug = model.Slug,
                Type = _typeConvertService.FromDTO(model.Type)
            };

            if (model.Content != null && model.Type != null)
            {
                var converter = _contentRegistry.GetConverter(model.Type.Name);
                if (page.Type.TypeCode == (int)PageType.Empty)
                {
                    page.JustPageContents.Add((JustPageContent)converter.FromDTO(model.Content));
                }
                else if (page.Type.TypeCode == (int)PageType.Board)
                {
                    page.Boards.Add((Board)converter.FromDTO(model.Content));
                }
                else if (page.Type.TypeCode == (int)PageType.List)
                {
                    page.Lists.Add((List)converter.FromDTO(model.Content));
                }
                else if (page.Type.TypeCode == (int)PageType.Calendar)
                {
                    page.Calendars.Add((Calendar)converter.FromDTO(model.Content));
                }
                else if (page.Type.TypeCode == (int)PageType.Gallery)
                {
                    page.Galleries.Add((Gallery)converter.FromDTO(model.Content));
                }
                else if (page.Type.TypeCode == (int)PageType.Table)
                {
                    page.Tables.Add((Table)converter.FromDTO(model.Content));
                }
            }
            return page;
        }

        public PageDTO ToDTO(DAL.Models.Page model)
        {
            var page = new PageDTO()
            {
                Id = model.Id,
                Banner = model.Banner,
                Icon = model.Icon,
                OwnerId = (Guid)model.OwnerId,
                Title = model.Title,
                CreatedAt = model.CreatedAt,
                DeleteDt = model.DeleteDt,
                Slug = model.Slug,
                Type = _typeConvertService.ToDTO(model.Type)
            };

            var converter = _contentRegistry.GetConverter(model.Type.Name);
            if (page.Type.TypeCode == (int)PageType.Empty)
            {
                page.Content = model.JustPageContents.FirstOrDefault();
            }
            else if (page.Type.TypeCode == (int)PageType.Board)
            {
                page.Content = model.Boards.FirstOrDefault();
            }
            else if (page.Type.TypeCode == (int)PageType.List)
            {
                page.Content = model.Lists.FirstOrDefault();
            }
            else if (page.Type.TypeCode == (int)PageType.Calendar)
            {
                page.Content = model.Calendars.FirstOrDefault();
            }
            else if (page.Type.TypeCode == (int)PageType.Gallery)
            {
                page.Content = model.Galleries.FirstOrDefault();
            }
            else if (page.Type.TypeCode == (int)PageType.Table)
            {
                page.Content = model.Tables.FirstOrDefault();
            }

            return page;
        }
    }
}
