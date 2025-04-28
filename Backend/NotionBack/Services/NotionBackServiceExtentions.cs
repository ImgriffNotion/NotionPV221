using NotionBack.DAL.Models;
using NotionBack.DAL.Models.fileStructure;
using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;
using NotionBack.Services.ContentConverterService;
using NotionBack.Services.ConverterService;
using NotionBack.Services.ConverterService.Files;
using NotionBack.Services.ConverterService.Page;
using NotionBack.Services.ConverterService.TypeBoard;
using NotionBack.Services.ConverterService.TypeCalendar;
using NotionBack.Services.ConverterService.TypeEmpty;
using NotionBack.Services.ConverterService.TypeGallery;
using NotionBack.Services.ConverterService.TypeList;
using NotionBack.Services.ConverterService.TypeTable;
using NotionBack.Services.ConverterService.UntypeContentService;
using NotionBack.Services.ConverterService.Users;
using NotionBack.Services.EmailAuthorizationService;
using NotionBack.Services.EmailService;
using NotionBack.Services.PageTypesService;
using NotionBack.Services.RandomService;

namespace NotionBack.Services
{
    public static class NotionBackServiceExtentions
    {
        public static IServiceCollection RegistatorAllServices(this IServiceCollection services)
        {
            #region IConvertServices

            // Page
            services.AddScoped<IConvertService<UserDTO, User>, UserConverter>();

            // Page
            services.AddScoped<IConvertService<PageDTO, Page>, PageConverter>();
            services.AddScoped<IConvertService<PageTypeDTO, TypePage>, PageTypeConverter>();

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

            services.AddScoped<ContentConverterRegistryInitializer>();

            #endregion

            services.AddScoped<IPageTypeService, PageTypeService>();

            services.AddSingleton<IRandomService, RandomCreatorService>();
            services.AddSingleton<IEmailService, EmailSenderService>();
            services.AddScoped<IEmailAuthService, EmailAuthService>();
            services.AddHttpClient();
            return services;
        }
    }
}
