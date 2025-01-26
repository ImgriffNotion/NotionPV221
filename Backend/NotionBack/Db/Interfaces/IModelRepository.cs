namespace NotionBack.Db.Interfaces
{
    public interface IModelRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> Get(Guid id);

        Task Create(T item);

        void Update(T item);

        Task Delete(Guid id);
    }
}
