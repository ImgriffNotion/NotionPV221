using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;
using NotionBack.Models.Enums;

namespace NotionBack.Services.PageContent
{
    public class PageContentService(IUnitOfWork unitOfWork) : IPageContentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        
        public async Task<Page> GetContent(Page page)
        {
            if (page == null)
                return new Page();

            if (page.Type != null)
            {

                switch ((PageType)page.Type.TypeCode)
                {
                    case PageType.Empty:
                        {
                            await _unitOfWork.JustPageContents.GetAll();
                            break;
                        }
                    case PageType.Board:
                        {
                            var tmp = await _unitOfWork.Boards.GetAll();
                            await _unitOfWork.Lists.GetAll();
                            await _unitOfWork.ListContents.GetAll();
                            break;
                        }
                    case PageType.List:
                        {
                            await _unitOfWork.Lists.GetAll();
                            await _unitOfWork.ListContents.GetAll();
                            break;
                        }
                    case PageType.Calendar:
                        {
                            await _unitOfWork.Calendars.GetAll(page.Id);
                            await _unitOfWork.CalendarContents.GetAll();
                            break;
                        }
                    case PageType.Gallery:
                        {
                            await _unitOfWork.Galleries.GetAll();
                            await _unitOfWork.GalleryContents.GetAll();
                            break;
                        }
                    case PageType.Table:
                        {
                            await _unitOfWork.Tables.GetAll();
                            await _unitOfWork.TableContents.GetAll();
                            break;
                        }
                };
            }
            return page;
        }
    }
}
