namespace NotionBack.Services.ConverterService.UntypeContentService
{
    public interface IContentConverterRegistry
    {
        IUntypedConvertService GetConverter(string contentType);
        void RegisterConverter(string contentType, IUntypedConvertService converter);
    }
}
