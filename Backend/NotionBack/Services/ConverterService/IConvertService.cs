namespace NotionBack.Services.ConverterService
{
    public interface IConvertService<TDto, TDomain>
    {
        public Task<TDto> ToDTO(TDomain model);
        public Task<TDomain> FromDTO (TDto model);
        public Task<TDomain> FromDTO (TDomain domain, TDto dto);
    }
}
