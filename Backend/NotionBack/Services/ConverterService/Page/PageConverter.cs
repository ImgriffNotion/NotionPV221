using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;
using NotionBack.Services.PageTypesService;
using System.Text.Json;

namespace NotionBack.Services.ConverterService.Page
{
    public class PageConverter(
        IConvertService<PageTypeDTO, TypePage> typeConvertService, IPageTypeService pageTypeService,
        IConvertService<BoardDTO, Board> boardConvertService,
        IConvertService<ListDTO, List> listConvertService,
        IConvertService<TableDTO, Table> tableConvertService,
        IConvertService<GalleryDTO, Gallery> galleryConvertService,
        IConvertService<CalendarDTO, Calendar> calendarConvertService,
        IConvertService<EmptyPageContentDTO, JustPageContent> emptyConvertService) : IConvertService<PageDTO, NotionBack.DAL.Models.Page>
    {
        private readonly IPageTypeService _pageTypeService = pageTypeService;
        private readonly IConvertService<PageTypeDTO, TypePage> _typeConvertService = typeConvertService;
        private readonly IConvertService<BoardDTO, Board> _boardConvertService = boardConvertService;
        private readonly IConvertService<ListDTO, List> _listConvertService = listConvertService;
        private readonly IConvertService<TableDTO, Table> _tableConvertService = tableConvertService;
        private readonly IConvertService<GalleryDTO, Gallery> _galleryConvertService = galleryConvertService;
        private readonly IConvertService<CalendarDTO, Calendar> _calendarConvertService = calendarConvertService;
        private readonly IConvertService<EmptyPageContentDTO, JustPageContent> _emptyConvertService = emptyConvertService;


        public async Task<DAL.Models.Page> FromDTO(PageDTO model)
        {
            if (model == null)
                return new DAL.Models.Page();

            var page = new DAL.Models.Page()
            {
                Id = model.Id,
                Banner = model.Banner,
                Icon = model.Icon,
                OwnerId = model.OwnerId,
                Title = model.Title,
                Slug = model.Slug
            };

            if (model.Content != null && model.Type != null)
            {
                var contentElement = JsonSerializer.SerializeToElement(model.Content);
                switch ((PageType)_pageTypeService.GetCodeOfPageType(model.Type))
                {
                    case PageType.Empty:
                        {
                            page.JustPageContents = new List<JustPageContent>();
                            model.Content = JsonSerializer.Deserialize<EmptyPageContentDTO>(contentElement.GetRawText());
                            if (model.Content != null)
                                page.JustPageContents.Add(await _emptyConvertService.FromDTO((EmptyPageContentDTO)model.Content));
                            break;
                        }
                    case PageType.Board:
                        {
                            page.Boards = new List<Board>();
                            model.Content = JsonSerializer.Deserialize<BoardDTO>(contentElement.GetRawText());
                            if (model.Content != null)
                                page.Boards.Add(await _boardConvertService.FromDTO((BoardDTO)model.Content));
                            break;
                        }
                    case PageType.List:
                        {
                            page.Lists = new List<List>();
                            model.Content = JsonSerializer.Deserialize<ListDTO>(contentElement.GetRawText());
                            if (model.Content != null)
                                page.Lists.Add(await _listConvertService.FromDTO((ListDTO)model.Content));
                            break;
                        }
                    case PageType.Calendar:
                        {
                            page.Calendars = new List<Calendar>();
                            model.Content = JsonSerializer.Deserialize<CalendarDTO>(contentElement.GetRawText());
                            if (model.Content != null)
                                page.Calendars.Add(await _calendarConvertService.FromDTO((CalendarDTO)model.Content));
                            break;
                        }
                    case PageType.Gallery:
                        {
                            page.Galleries = new List<Gallery>();
                            model.Content = JsonSerializer.Deserialize<GalleryDTO>(contentElement.GetRawText());
                            if (model.Content != null)
                                page.Galleries.Add(await _galleryConvertService.FromDTO((GalleryDTO)model.Content));
                            break;
                        }
                    case PageType.Table:
                        {
                            page.Tables = new List<Table>();
                            model.Content = JsonSerializer.Deserialize<TableDTO>(contentElement.GetRawText());
                            if (model.Content != null)
                                page.Tables.Add(await _tableConvertService.FromDTO((TableDTO)model.Content));
                            break;
                        }

                };

            }
            return page;
        }

        public async Task<DAL.Models.Page> FromDTO(DAL.Models.Page domain, PageDTO dto)
        {
            if (domain == null || dto == null)
                return new DAL.Models.Page();

            domain.Title = dto.Title;
            domain.Banner = dto.Banner;
            domain.Icon = dto.Icon;
            domain.Slug = dto.Slug;

            if (dto.Content != null && dto.Type != null)
            {
                var contentElement = JsonSerializer.SerializeToElement(dto.Content);

                switch ((PageType)_pageTypeService.GetCodeOfPageType(dto.Type))
                {
                    case PageType.Empty:
                        {
                            dto.Content = JsonSerializer.Deserialize<EmptyPageContentDTO>(contentElement.GetRawText());
                            var domainContent = domain.JustPageContents.FirstOrDefault();
                            if (dto.Content != null)
                            {
                                if (domainContent != null)
                                    domainContent = await _emptyConvertService.FromDTO(domainContent, (EmptyPageContentDTO)dto.Content);
                                else
                                {
                                    domainContent = await _emptyConvertService.FromDTO((EmptyPageContentDTO)dto.Content);
                                    domain.JustPageContents.Add(domainContent);
                                }
                            }
                            break;
                        }
                    case PageType.Board:
                        {
                            dto.Content = JsonSerializer.Deserialize<BoardDTO>(contentElement.GetRawText());
                            var domainContent = domain.Boards.FirstOrDefault();
                            if (dto.Content != null)
                            {
                                if (domainContent != null)
                                    domainContent = await _boardConvertService.FromDTO(domainContent, (BoardDTO)dto.Content);
                                else
                                {
                                    domainContent = await _boardConvertService.FromDTO((BoardDTO)dto.Content);
                                    domain.Boards.Add(domainContent);
                                }
                            }
                            break;
                        }
                    case PageType.List:
                        {
                            dto.Content = JsonSerializer.Deserialize<ListDTO>(contentElement.GetRawText());
                            var domainContent = domain.Lists.FirstOrDefault();
                            if (dto.Content != null)
                            {
                                if (domainContent != null)
                                    domainContent = await _listConvertService.FromDTO(domainContent, (ListDTO)dto.Content);
                                else
                                {
                                    domainContent = await _listConvertService.FromDTO((ListDTO)dto.Content);
                                    domain.Lists.Add(domainContent);
                                }
                            }
                            break;
                        }
                    case PageType.Calendar:
                        {
                            dto.Content = JsonSerializer.Deserialize<CalendarDTO>(contentElement.GetRawText());
                            var domainContent = domain.Calendars.FirstOrDefault();
                            if (dto.Content != null)
                            {
                                if (domainContent != null)
                                    domainContent = await _calendarConvertService.FromDTO(domainContent, (CalendarDTO)dto.Content);
                                else
                                {
                                    domainContent = await _calendarConvertService.FromDTO((CalendarDTO)dto.Content);
                                    domain.Calendars.Add(domainContent);
                                }
                            }
                            break;
                        }
                    case PageType.Gallery:
                        {
                            dto.Content = JsonSerializer.Deserialize<GalleryDTO>(contentElement.GetRawText());
                            var domainContent = domain.Galleries.FirstOrDefault();
                            if (dto.Content != null)
                            {
                                if (domainContent != null)
                                    domainContent = await _galleryConvertService.FromDTO(domainContent, (GalleryDTO)dto.Content);
                                else
                                {
                                    domainContent = await _galleryConvertService.FromDTO((GalleryDTO)dto.Content);
                                    domain.Galleries.Add(domainContent);
                                }
                            }
                            break;
                        }
                    case PageType.Table:
                        {
                            dto.Content = JsonSerializer.Deserialize<TableDTO>(contentElement.GetRawText());
                            var domainContent = domain.Tables.FirstOrDefault();
                            if (dto.Content != null)
                            {
                                if (domainContent != null)
                                    domainContent = await _tableConvertService.FromDTO(domainContent, (TableDTO)dto.Content);
                                else
                                {
                                    domainContent = await _tableConvertService.FromDTO((TableDTO)dto.Content);
                                    domain.Tables.Add(domainContent);
                                }
                            }
                            break;
                        }

                };

            }

            return domain;
        }

        public async Task<PageDTO> ToDTO(DAL.Models.Page model)
        {
            if (model == null)
                return new PageDTO();

            var page = new PageDTO()
            {
                Id = model.Id,
                Banner = model.Banner,
                Icon = model.Icon,
                OwnerId = model.OwnerId,
                Title = model.Title,
                CreatedAt = model.CreatedAt,
                DeleteDt = model.DeleteDt,
                Slug = model.Slug,
                Type = model?.Type?.Name

            };


            switch ((PageType)_pageTypeService.GetCodeOfPageType(page.Type ?? ""))
            {
                case PageType.Empty:
                    {
                        var content = model?.JustPageContents.FirstOrDefault();
                        if (content != null)
                            page.Content = await _emptyConvertService.ToDTO(content);
                        break;
                    }
                case PageType.Board:
                    {
                        var content = model?.Boards.FirstOrDefault();
                        if (content != null) 
                            page.Content = await _boardConvertService.ToDTO(content);
                        break;
                    }
                case PageType.List:
                    {
                        var content = model?.Lists.FirstOrDefault();
                        if (content != null) page.Content = await _listConvertService.ToDTO(content);
                        break;
                    }
                case PageType.Calendar:
                    {
                        var content = model?.Calendars.FirstOrDefault();
                        if (content != null) page.Content = await _calendarConvertService.ToDTO(content);
                        break;
                    }
                case PageType.Gallery:
                    {
                        var content = model?.Galleries.FirstOrDefault();
                        if (content != null) page.Content = await _galleryConvertService.ToDTO(content);
                        break;
                    }
                case PageType.Table:
                    {
                        var content = model?.Tables.FirstOrDefault();
                        if (content != null) page.Content = await _tableConvertService.ToDTO(content);
                        break;
                    }
            };


            return page;
        }
    }
}
