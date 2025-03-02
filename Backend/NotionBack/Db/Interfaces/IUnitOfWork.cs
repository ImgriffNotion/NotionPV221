using NotionBack.Db.Models;

namespace NotionBack.Db.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IPageRepository Pages { get; }
        IFileRepository Files { get; }
        IListRepository Lists { get; }
        IBoardRepository Boards { get; }
        ITableRepository Tables { get; }
        IJustPageContentRepository JustPageContents { get; }
        ICalendarContentRepository CalendarContents { get; }
        IGalleryContentRepository GalleryContents { get; }
        ITableContentRepository TableContents { get; }
        IListContentReopsitory ListContents { get; }
        ITypePageRepository PageTypes { get; }
        ICalendarRepository Calendars { get; }
        IGalleryRepository Galleries { get; }
        Task Save();
    }
}
