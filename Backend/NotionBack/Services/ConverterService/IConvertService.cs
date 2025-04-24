namespace NotionBack.Services.ConverterService
{
    public interface IConvertService<TDto, TDomain>
    {
        public TDto ToDTO(TDomain model);
        public TDomain FromDTO (TDto model);
    }
}
