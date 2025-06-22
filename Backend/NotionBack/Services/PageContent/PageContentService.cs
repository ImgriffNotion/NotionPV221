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
                            await _unitOfWork.JustPageContents.GetAll(page.Id);
                            break;
                        }
                    case PageType.Board:
                        {
                            var board = (await _unitOfWork.Boards.GetAll(page.Id)).First();
                            try
                            {
                                foreach (var list in board.Lists)
                                {
                                    await _unitOfWork.ListContents.GetAll(list.Id);
                                }
                            }
                            catch (Exception ex) { Console.WriteLine($"\n\n\n WARNING !!!\n\n{ex.Message}\n\n\n"); }
                            break;
                        }
                    case PageType.List:
                        {
                            var list = (await _unitOfWork.Lists.GetAll(page.Id)).First();
                            await _unitOfWork.ListContents.GetAll(list.Id);
                            break;
                        }
                    case PageType.Calendar:
                        {
                            var calendar = (await _unitOfWork.Calendars.GetAll(page.Id)).First();
                            var contents = await _unitOfWork.CalendarContents.GetAll(calendar.Id);
                            break;
                        }
                    case PageType.Gallery:
                        {
                            var gallery = (await _unitOfWork.Galleries.GetAll(page.Id)).First();
                            await _unitOfWork.GalleryContents.GetAll(gallery.Id);
                            break;
                        }
                    case PageType.Table:
                        {
                            var table = (await _unitOfWork.Tables.GetAll(page.Id)).First();
                            await _unitOfWork.TableContents.GetAll(table.Id);
                            break;
                        }
                };
            }
            return page;
        }
    }
}
