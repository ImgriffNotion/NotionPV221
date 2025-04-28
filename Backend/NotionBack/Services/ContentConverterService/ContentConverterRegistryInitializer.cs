using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Services.ConverterService.UntypeContentService;
using NotionBack.Services.ConverterService;

namespace NotionBack.Services.ContentConverterService
{
    public class ContentConverterRegistryInitializer
    {
        private readonly ContentConverterRegistry _registry;
        private readonly IServiceProvider _provider;

        public ContentConverterRegistryInitializer(IServiceProvider provider)
        {
            _provider = provider;
            _registry = (ContentConverterRegistry)provider.GetRequiredService<IContentConverterRegistry>();
        }

        public void Initialize()
        {
            _registry.RegisterConverter(PageType.Empty.ToString(), new ConvertServiceWrapper<EmptyPageContentDTO, JustPageContent>(
                _provider.GetRequiredService<IConvertService<EmptyPageContentDTO, JustPageContent>>()));

            _registry.RegisterConverter(PageType.Gallery.ToString(), new ConvertServiceWrapper<GalleryDTO, Gallery>(
                _provider.GetRequiredService<IConvertService<GalleryDTO, Gallery>>()));

            _registry.RegisterConverter(PageType.Board.ToString(), new ConvertServiceWrapper<BoardDTO, Board>(
                _provider.GetRequiredService<IConvertService<BoardDTO, Board>>()));

            _registry.RegisterConverter(PageType.List.ToString(), new ConvertServiceWrapper<ListDTO, DAL.Models.pageContents.List>(
                _provider.GetRequiredService<IConvertService<ListDTO, DAL.Models.pageContents.List>>()));

            _registry.RegisterConverter(PageType.Calendar.ToString(), new ConvertServiceWrapper<CalendarDTO, Calendar>(
                _provider.GetRequiredService<IConvertService<CalendarDTO, Calendar>>()));

            _registry.RegisterConverter(PageType.Table.ToString(), new ConvertServiceWrapper<TableDTO, Table>(
                _provider.GetRequiredService<IConvertService<TableDTO, Table>>()));
        }
    }
}
