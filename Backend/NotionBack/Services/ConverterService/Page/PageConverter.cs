using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Services.ConverterService.UntypeContentService;
using System.Text.Json;

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
                Slug = model.Slug,
                Type = _typeConvertService.FromDTO(model.Type),
                Boards = new List<Board>(),
                Lists = new List<List>(),
                Tables = new List<Table>(),
                Calendars = new List<Calendar>(),
                Galleries = new List<Gallery>(),
                JustPageContents = new List<JustPageContent>()
            };

            if (model.Content != null && model.Type != null)
            {
                var content = model.Content;
                var contentElement = JsonSerializer.SerializeToElement(model.Content);
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
                            var boardDto = JsonSerializer.Deserialize<BoardDTO>(contentElement.GetRawText());
                            model.Content = boardDto;
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
            object content = null;
            
            switch ((PageType)page.Type.TypeCode)
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
