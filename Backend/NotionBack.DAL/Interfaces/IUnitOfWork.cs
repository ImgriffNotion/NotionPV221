namespace NotionBack.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IPageRepository Pages { get; }
        IFileRepository Files { get; }
        IListRepository Lists { get; }
        ITokenRepository Tokens { get; }
        IBoardRepository Boards { get; }
        ITableRepository Tables { get; }
        IGalleryRepository Galleries { get; }
        ITypePageRepository PageTypes { get; }
        ICalendarRepository Calendars { get; }
        IListContentReopsitory ListContents { get; }
        ITableContentRepository TableContents { get; }
        IGalleryContentRepository GalleryContents { get; }
        IJustPageContentRepository JustPageContents { get; }
        ICalendarContentRepository CalendarContents { get; }

        Task Save();
    }
}
