using NotionBack.DAL.Interfaces.Templates;

namespace NotionBack.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        #region Data
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
        IListContentRepository ListContents { get; }
        ITableContentRepository TableContents { get; }
        IGalleryContentRepository GalleryContents { get; }
        IJustPageContentRepository JustPageContents { get; }
        ICalendarContentRepository CalendarContents { get; }
        #endregion

        #region Templates
        ITemplateRepository Templates { get; }
        IListTemplateRepository ListTemplates { get; }
        IBoardTemplateRepository BoardTemplates { get; }
        ITableTemplateRepository TableTemplates { get; }
        IGalleryTemplateRepository GallerieTemplates { get; }
        ITypePageTemplateRepository PageTypeTemplates { get; }
        ICalendarTemplateRepository CalendarTemplates { get; }
        IListContentTemplateRepository ListContentTemplates { get; }
        ITableContentTemplateRepository TableContentTemplates { get; }
        IGalleryContentTemplateRepository GalleryContentTemplates { get; }
        IJustPageContentTemplateRepository JustPageContentTemplates { get; }
        ICalendarContentTemplateRepository CalendarContentTemplates { get; }
        #endregion

        Task Save();
    }
}
