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
                switch ((PageType)page.Type.TypeCode)
                {
                    case PageType.Empty:
                        {
                            page.JustPageContents.Add((JustPageContent)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.Board:
                        {
                            page.Boards.Add((Board)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.List:
                        {
                            page.Lists.Add((List)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.Calendar:
                        {
                            page.Calendars.Add((Calendar)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.Gallery:
                        {
                            page.Galleries.Add((Gallery)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.Table:
                        {
                            page.Tables.Add((Table)converter.FromDTO(model.Content));
                            break;
                        }

                };

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

            switch ((PageType)page.Type.TypeCode)
            {
                case PageType.Empty:
                    {
                        page.Content = converter.ToDTO(model.JustPageContents.FirstOrDefault());
                        break;
                    }
                case PageType.Board:
                    {
                        page.Content = converter.ToDTO(model.Boards.FirstOrDefault());
                        break;
                    }
                case PageType.List:
                    {
                        page.Content = converter.ToDTO(model.Lists.FirstOrDefault());
                        break;
                    }
                case PageType.Calendar:
                    {
                        page.Content =converter.ToDTO( model.Calendars.FirstOrDefault());
                        break;
                    }
                case PageType.Gallery:
                    {
                        page.Content = converter.ToDTO(model.Galleries.FirstOrDefault());
                        break;
                    }
                case PageType.Table:
                    {
                        page.Content = converter.ToDTO(model.Tables.FirstOrDefault());
                        break;
                    }
            };
            return page;
        }
    }
}
