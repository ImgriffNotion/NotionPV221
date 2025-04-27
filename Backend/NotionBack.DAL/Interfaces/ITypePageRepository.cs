using NotionBack.DAL.Models;

namespace NotionBack.DAL.Interfaces;

public interface ITypePageRepository : IModelRepository<TypePage>
{
  Task<TypePage> GetTypePageByCode(int code);
  Task<TypePage> GetTypePageByName(String name);
}
