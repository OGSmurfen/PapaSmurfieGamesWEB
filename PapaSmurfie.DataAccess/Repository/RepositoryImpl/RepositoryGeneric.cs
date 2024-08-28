
using Microsoft.EntityFrameworkCore;
using PapaSmurfie.Database;
using PapaSmurfie.Repository.IRepository;
using System.Linq.Expressions;

namespace PapaSmurfie.Repository.RepositoryImpl
{
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public RepositoryGeneric(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }


        public async Task CreateAsync(TEntity e)
        {
            await dbSet.AddAsync(e);
        }

        public void Delete(TEntity e)
        {
           dbSet.Remove(e);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<TEntity> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }
            
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<TEntity> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }


        public void Update(TEntity e)
        {
            dbSet.Update(e);
        }
    }
}
