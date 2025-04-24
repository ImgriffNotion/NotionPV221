namespace NotionBack.Services.ConverterService.UntypeContentService
{
    public class ConvertServiceWrapper<TDto, TModel>(IConvertService<TDto, TModel> service) : IUntypedConvertService
    {
        private readonly IConvertService<TDto, TModel> _service = service;

        public object FromDTO(object dto) => _service.FromDTO((TDto)dto);
        public object ToDTO(object model) => _service.ToDTO((TModel)model);
    }
}
