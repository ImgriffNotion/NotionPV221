using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Repositories.pageContents;
using NotionBack.DAL.Repositories.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories
{
    public class EfUnitOfWork(NotionDbContext context) : IUnitOfWork
    {
        private readonly NotionDbContext context = context;

        #region Repositories Definitions
        private UsersRepository?           _usersRepository;
        private PageRepository?            _pageRepository;
        private TypePageRepository?        _pageTypesRepository;
        private TableRepository?           _tableRepository;
        private TableContentRepository?    _tableContentRepository;
        private ListRepository?            _listRepository;
        private ListContentRepository?     _listContentRepository;
        private JustPageContentRepository? _justPageContentRepository;
        private GalleryRepository?         _galleryRepository;
        private GalleryContentRepository?  _galleryContentRepository;
        private FileRepository?            _fileRepository;
        private CalendarRepository?        _calendarRepository;
        private CalendarContentRepository? _calendarContentRepository;
        private BoardRepository?           _boardRepository;
        #endregion


        #region Repositories Initialization
        public IUserRepository  Users  => _usersRepository ??= new UsersRepository(context);
        public IPageRepository  Pages  => _pageRepository  ??= new PageRepository(context);
        public IFileRepository  Files  => _fileRepository  ??= new FileRepository(context);
        public IListRepository  Lists  => _listRepository  ??= new ListRepository(context);
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
        public IListContentReopsitory ListContents =>
            _listContentRepository ??= new ListContentRepository(context);
        public ITypePageRepository PageTypes =>
            _pageTypesRepository ??= new TypePageRepository(context);
        public ICalendarRepository Calendars =>
            _calendarRepository ??= new CalendarRepository(context);
        public IGalleryRepository Galleries =>
            _galleryRepository ??= new GalleryRepository(context);
        #endregion
        public async Task Save() => await context.SaveChangesAsync();
    }
}
