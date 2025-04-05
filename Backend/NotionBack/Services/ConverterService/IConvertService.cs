namespace NotionBack.Services.ConverterService
{
    public interface IConvertService<T, T2>
    {
        public T ToDTO(T2 model);
        public T2 FromDTO (T model);
    }
}
