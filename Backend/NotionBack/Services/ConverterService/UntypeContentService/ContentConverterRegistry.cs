namespace NotionBack.Services.ConverterService.UntypeContentService
{
    public class ContentConverterRegistry : IContentConverterRegistry
    {

        private readonly Dictionary<String, IUntypedConvertService> _registry = new();

        public IUntypedConvertService GetConverter(string contentType)
        {
            if(_registry.TryGetValue(contentType, out IUntypedConvertService converter))
            {
                return converter;
            }

            return null;
        }

        public void RegisterConverter(string contentType, IUntypedConvertService converter)
        {
            _registry[contentType] = converter;
        }
    }
}
