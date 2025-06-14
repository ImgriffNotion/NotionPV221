using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Repositories.pageContents;
using NotionBack.DAL.Repositories.pageContents.pageInPageContents;
using NotionBack.DAL.Repositories.Templates;
using NotionBack.DAL.Repositories.Templates.pageContents;
using NotionBack.DAL.Repositories.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories
{
    public class EfUnitOfWork(NotionDbContext context) : IUnitOfWork
    {
        private readonly NotionDbContext context = context;

        #region Data repositories Definitions
        private TokenRepository? _tokenRepository;
        private UsersRepository? _usersRepository;
        private PageRepository? _pageRepository;
        private TypePageRepository? _pageTypesRepository;
        private TableRepository? _tableRepository;
        private TableContentRepository? _tableContentRepository;
        private ListRepository? _listRepository;
        private ListContentRepository? _listContentRepository;
        private JustPageContentRepository? _justPageContentRepository;
        private GalleryRepository? _galleryRepository;
        private GalleryContentRepository? _galleryContentRepository;
        private FileRepository? _fileRepository;
        private CalendarRepository? _calendarRepository;
        private CalendarContentRepository? _calendarContentRepository;
        private BoardRepository? _boardRepository;
        #endregion

        #region Templates repositories Definitions
        private TemplateRepository? _templateRepository;
        private TypePageTemplateRepository? _pageTypeTemplatesRepository;
        private TableTemplateRepository? _tableTemplateRepository;
        private TableContentTemplateRepository? _tableContentTemplateRepository;
        private ListTemplateRepository? _listTemplateRepository;
        private ListContentTemplateRepository? _listContentTemplateRepository;
        private JustPageContentTemplateRepository? _justPageContentTemplateRepository;
        private GalleryTemplateRepository? _galleryTemplateRepository;
        private GalleryContentTemplateRepository? _galleryContentTemplateRepository;
        private CalendarTemplateRepository? _calendarTemplateRepository;
        private CalendarContentTemplateRepository? _calendarContentTemplateRepository;
        private BoardTemplateRepository? _boardTemplateRepository;
        #endregion

        #region Data repositories Initialization
        public ITokenRepository Tokens => _tokenRepository ??= new TokenRepository(context);
        public IUserRepository Users => _usersRepository ??= new UsersRepository(context);
        public IPageRepository Pages => _pageRepository ??= new PageRepository(context);
        public IFileRepository Files => _fileRepository ??= new FileRepository(context);
        public IListRepository Lists => _listRepository ??= new ListRepository(context);
        public IBoardRepository Boards => _boardRepository ??= new BoardRepository(context);
        public ITableRepository Tables => _tableRepository ??= new TableRepository(context);
        public IJustPageContentRepository JustPageContents =>
            _justPageContentRepository ??= new JustPageContentRepository(context);
        public ICalendarContentRepository CalendarContents =>
            _calendarContentRepository ??= new CalendarContentRepository(context);
        public IGalleryContentRepository GalleryContents =>
            _galleryContentRepository ??= new GalleryContentRepository(context);
        public ITableContentRepository TableContents =>
            _tableContentRepository ??= new TableContentRepository(context);
        public IListContentRepository ListContents =>
            _listContentRepository ??= new ListContentRepository(context);
        public ITypePageRepository PageTypes =>
            _pageTypesRepository ??= new TypePageRepository(context);
        public ICalendarRepository Calendars =>
            _calendarRepository ??= new CalendarRepository(context);
        public IGalleryRepository Galleries =>
            _galleryRepository ??= new GalleryRepository(context);
        #endregion


        #region Templates repositories Initialization
        public ITemplateRepository Templates =>
            _templateRepository ??= new TemplateRepository(context);
        public IListTemplateRepository ListTemplates =>
            _listTemplateRepository ??= new ListTemplateRepository(context);
        public IBoardTemplateRepository BoardTemplates =>
            _boardTemplateRepository ??= new BoardTemplateRepository(context);
        public ITableTemplateRepository TableTemplates =>
            _tableTemplateRepository ??= new TableTemplateRepository(context);
        public IJustPageContentTemplateRepository JustPageContentTemplates =>
            _justPageContentTemplateRepository ??= new JustPageContentTemplateRepository(context);
        public ICalendarContentTemplateRepository CalendarContentTemplates =>
            _calendarContentTemplateRepository ??= new CalendarContentTemplateRepository(context);
        public IGalleryContentTemplateRepository GalleryContentTemplates =>
            _galleryContentTemplateRepository ??= new GalleryContentTemplateRepository(context);
        public ITableContentTemplateRepository TableContentTemplates =>
            _tableContentTemplateRepository ??= new TableContentTemplateRepository(context);
        public IListContentTemplateRepository ListContentTemplates =>
            _listContentTemplateRepository ??= new ListContentTemplateRepository(context);
        public ITypePageTemplateRepository PageTypeTemplates =>
            _pageTypeTemplatesRepository ??= new TypePageTemplateRepository(context);
        public ICalendarTemplateRepository CalendarTemplates =>
            _calendarTemplateRepository ??= new CalendarTemplateRepository(context);
        public IGalleryTemplateRepository GallerieTemplates =>
            _galleryTemplateRepository ??= new GalleryTemplateRepository(context);
        #endregion

        public async Task Save() => await context.SaveChangesAsync();
    }
}
