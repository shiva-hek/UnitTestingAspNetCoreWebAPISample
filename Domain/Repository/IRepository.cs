namespace UserDomain.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void RemoveAsync(TEntity entity);
        Task Save();
    }
}