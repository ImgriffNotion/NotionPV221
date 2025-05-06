namespace NotionBack.Services.ConverterService.UntypeContentService
{
    public interface IUntypedConvertService
    {
        object FromDTO(object dto);
        object ToDTO(object model);
        object FromDTO(object domain, object dto);
    }
}
