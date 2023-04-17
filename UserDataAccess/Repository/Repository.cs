using Microsoft.EntityFrameworkCore;
using UserDataAccess.Repository.Exceptions;
using UserDomain.Repository;

namespace UserDataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly UserDbContext Context;

        public Repository(UserDbContext context)
        {
            Context = context;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var result = await Context.Set<TEntity>().FindAsync(id);

            if (result == null)
                throw new FailedToFindEntityByIdException();
            else
                return result;
        }

        /// <exception cref="OperationCanceledException"></exception>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await Context.Set<TEntity>().ToListAsync();
            }
            catch
            {
                throw new OperationCanceledException();
            }
            
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public void RemoveAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public async Task Save()
        {
            Context.SaveChangesAsync();
        }
    }
}

