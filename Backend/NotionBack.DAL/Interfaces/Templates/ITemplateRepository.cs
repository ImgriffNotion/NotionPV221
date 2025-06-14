using NotionBack.DAL.Models.Templates;

namespace NotionBack.DAL.Interfaces.Templates;

public interface ITemplateRepository : IModelRepository<Template>
{
    Task DeleteTemplatePermanently(Template Template);
}
