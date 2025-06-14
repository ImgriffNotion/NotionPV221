using NotionBack.DAL.Models.Templates;

namespace NotionBack.DAL.Interfaces.Templates;

public interface ITypePageTemplateRepository : IModelRepository<TypePageTemplate>
{
    Task<TypePageTemplate> GetTypePageTemplateByCode(int code);
    Task<TypePageTemplate> GetTypePageTemplateByName(String name);
}
