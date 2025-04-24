using NotionBack.DAL.Models.fileStructure;
using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;
using NotionBack.Services.ConverterService;
using NotionBack.Services.ConverterService.Files;
using NotionBack.Services.ConverterService.TypeBoard;
using NotionBack.Services.ConverterService.TypeCalendar;
using NotionBack.Services.ConverterService.TypeEmpty;
using NotionBack.Services.ConverterService.TypeGallery;
using NotionBack.Services.ConverterService.TypeList;
using NotionBack.Services.ConverterService.TypeTable;
using NotionBack.Services.ConverterService.UntypeContentService;
using NotionBack.Services.EmailAuthorizationService;
using NotionBack.Services.EmailService;
using NotionBack.Services.RandomService;

namespace NotionBack.Services
{
    public static class NotionBackServiceExtentions
    {
        public static IServiceCollection RegistatorAllServices(this IServiceCollection services)
        {
            #region IConvertServices

            // Files
            services.AddScoped<IConvertService<FileDTO, DAL.Models.fileStructure.File>, FileConverter>();

            // Table
            services.AddScoped<IConvertService<TableDTO, Table>, TableConverter>();
            services.AddScoped<IConvertService<TableContentDTO, TableContent>, TableContentConverter>();

            // Gallery
            services.AddScoped<IConvertService<GalleryDTO, Gallery>, GalleryConverter>();
            services.AddScoped<IConvertService<GalleryContentDTO, GalleryContent>, GalleryContentConverter>();

            // Empty
            services.AddScoped<IConvertService<EmptyPageContentDTO, JustPageContent>, EmptyPageConverter>();

            // Calendar
            services.AddScoped<IConvertService<CalendarDTO, Calendar>, CalendarConverter>();
            services.AddScoped<IConvertService<CalendarContentDTO, CalendarContent>, CalendarContentConverter>();

            // Board
            services.AddScoped<IConvertService<BoardDTO, Board>, BoardConverter>();

            // List
            services.AddScoped<IConvertService<ListDTO, DAL.Models.pageContents.List>, ListConverter>();
            services.AddScoped<IConvertService<ListContentDTO, ListContent>, ListContentConverter>();

            #endregion

            #region Register contentConverters 

            services.AddSingleton<IContentConverterRegistry, ContentConverterRegistry>();

            var provider = services.BuildServiceProvider();
            var registry = (ContentConverterRegistry)provider.GetRequiredService<IContentConverterRegistry>();

            registry.RegisterConverter(PageType.Empty.ToString(), new ConvertServiceWrapper<EmptyPageContentDTO, JustPageContent>(
                provider.GetRequiredService<IConvertService<EmptyPageContentDTO, JustPageContent>>()));

            registry.RegisterConverter(PageType.Gallery.ToString(), new ConvertServiceWrapper<GalleryDTO, Gallery>(
                provider.GetRequiredService<IConvertService<GalleryDTO, Gallery>>()));

            registry.RegisterConverter(PageType.Board.ToString(), new ConvertServiceWrapper<BoardDTO, Board>(
                provider.GetRequiredService<IConvertService<BoardDTO, Board>>()));

            registry.RegisterConverter(PageType.List.ToString(), new ConvertServiceWrapper<ListDTO, List>(
                provider.GetRequiredService<IConvertService<ListDTO, List>>()));

            registry.RegisterConverter(PageType.Calendar.ToString(), new ConvertServiceWrapper<CalendarDTO, Calendar>(
                provider.GetRequiredService<IConvertService<CalendarDTO, Calendar>>()));

            registry.RegisterConverter(PageType.Table.ToString(), new ConvertServiceWrapper<TableDTO, Table>(
                provider.GetRequiredService<IConvertService<TableDTO, Table>>()));
            #endregion

            services.AddSingleton<IRandomService, RandomCreatorService>();
            services.AddSingleton<IEmailService, EmailSenderService>();
            services.AddScoped<IEmailAuthService, EmailAuthService>();
            services.AddHttpClient();
            return services;
        }
    }
}
