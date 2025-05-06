using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;
using NotionBack.Services.ConverterService.UntypeContentService;
using NotionBack.Services.PageTypesService;
using System.Text.Json;

namespace NotionBack.Services.ConverterService.Page
{
    public class PageConverter(IContentConverterRegistry registry, IConvertService<PageTypeDTO, TypePage> typeConvertService, IPageTypeService pageTypeService) : IConvertService<PageDTO, NotionBack.DAL.Models.Page>
    {
        private readonly IContentConverterRegistry _contentRegistry = registry;
        private readonly IConvertService<PageTypeDTO, TypePage> _typeConvertService = typeConvertService;
        private readonly IPageTypeService _pageTypeService = pageTypeService;


        public DAL.Models.Page FromDTO(PageDTO model)
        {
  
            var page = new DAL.Models.Page()
            {
                Id = model.Id,
                Banner = model.Banner,
                Icon = model.Icon,
                OwnerId = model.OwnerId,
                Title = model.Title,
                Slug = model.Slug,
                Boards = new List<Board>(),
                Lists = new List<List>(),
                Tables = new List<Table>(),
                Calendars = new List<Calendar>(),
                Galleries = new List<Gallery>(),
                JustPageContents = new List<JustPageContent>()
            };

            if (model.Content != null && model.Type != null)
            {
                var contentElement = JsonSerializer.SerializeToElement(model.Content);
                var converter = _contentRegistry.GetConverter(model.Type);
                switch ((PageType)_pageTypeService.GetCodeOfPageType(model.Type))
                {
                    case PageType.Empty:
                        {
                            model.Content = JsonSerializer.Deserialize<EmptyPageContentDTO>(contentElement.GetRawText());
                            page.JustPageContents.Add((JustPageContent)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.Board:
                        {
                            model.Content = JsonSerializer.Deserialize<BoardDTO>(contentElement.GetRawText());
                            page.Boards.Add((Board)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.List:
                        {
                            model.Content = JsonSerializer.Deserialize<ListDTO>(contentElement.GetRawText());
                            page.Lists.Add((List)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.Calendar: 
                        {
                            model.Content = JsonSerializer.Deserialize<CalendarDTO>(contentElement.GetRawText());
                            page.Calendars.Add((Calendar)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.Gallery:
                        {
                            model.Content = JsonSerializer.Deserialize<GalleryDTO>(contentElement.GetRawText()); 
                            page.Galleries.Add((Gallery)converter.FromDTO(model.Content));
                            break;
                        }
                    case PageType.Table:
                        {
                            model.Content = JsonSerializer.Deserialize<TableDTO>(contentElement.GetRawText()); 
                            page.Tables.Add((Table)converter.FromDTO(model.Content));
                            break;
                        }

                };

            }
            return page;
        }

        public DAL.Models.Page FromDTO(DAL.Models.Page domain, PageDTO dto)
        {
            domain.Title = dto.Title;
            domain.Banner = dto.Banner;
            domain.Icon = dto.Icon;
            domain.Slug = dto.Slug;

            if (dto.Content != null && dto.Type != null)
            {
                var converter = _contentRegistry.GetConverter(dto.Type);
                var contentElement = JsonSerializer.SerializeToElement(dto.Content);
                switch ((PageType)_pageTypeService.GetCodeOfPageType(dto.Type))
                {
                    case PageType.Empty:
                        {
                            dto.Content = JsonSerializer.Deserialize<EmptyPageContentDTO>(contentElement.GetRawText());
                            var domainContent = domain.JustPageContents.FirstOrDefault();
                            domainContent = (JustPageContent)converter.FromDTO(domainContent, dto.Content);
                            break;
                        }
                    case PageType.Board:
                        {
                            dto.Content = JsonSerializer.Deserialize<BoardDTO>(contentElement.GetRawText());
                            var domainContent = domain.Boards.FirstOrDefault();
                            domainContent = (Board)converter.FromDTO(domainContent, dto.Content);
                            break;
                        }
                    case PageType.List:
                        {
                            dto.Content = JsonSerializer.Deserialize<ListDTO>(contentElement.GetRawText());
                            var domainContent = domain.Lists.FirstOrDefault();
                            domainContent = (List)converter.FromDTO(domainContent, dto.Content);
                            break;
                        }
                    case PageType.Calendar:
                        {
                            dto.Content = JsonSerializer.Deserialize<CalendarDTO>(contentElement.GetRawText());
                            var domainContent = domain.Calendars.FirstOrDefault();
                            domainContent = (Calendar)converter.FromDTO(domainContent, dto.Content);
                            break;
                        }
                    case PageType.Gallery:
                        {
                            dto.Content = JsonSerializer.Deserialize<GalleryDTO>(contentElement.GetRawText());
                            var domainContent = domain.Galleries.FirstOrDefault();
                            domainContent = (Gallery)converter.FromDTO(domainContent, dto.Content);
                            break;
                        }
                    case PageType.Table:
                        {
                            dto.Content = JsonSerializer.Deserialize<TableDTO>(contentElement.GetRawText());
                            var domainContent = domain.Tables.FirstOrDefault();
                            domainContent = (Table)converter.FromDTO(domainContent, dto.Content);
                            break;
                        }

                };

            }

            return domain;
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
                Type = model.Type.Name

            };

            var converter = _contentRegistry.GetConverter(model.Type.Name);
            object content = null;
            
            switch ((PageType)_pageTypeService.GetCodeOfPageType(page.Type))
            {
                case PageType.Empty:
                    {
                        content = model.JustPageContents.FirstOrDefault();
                        break;
                    }
                case PageType.Board:
                    {
                        content = model.Boards.FirstOrDefault();
                        break;
                    }
                case PageType.List:
                    {
                        content = model.Lists.FirstOrDefault();
                        break;
                    }
                case PageType.Calendar:
                    {
                        content = model.Calendars.FirstOrDefault();
                        break;
                    }
                case PageType.Gallery:
                    {
                        content = model.Galleries.FirstOrDefault();
                        break;
                    }
                case PageType.Table:
                    {
                        content = model.Tables.FirstOrDefault();
                        break;
                    }
            };

            if (content != null)
                page.Content = converter.ToDTO(content);

            return page;
        }
    }
}
