using System.Linq.Expressions;

namespace PapaSmurfie.Repository.IRepository
{
    public interface IRepositoryGeneric<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true);
        Task CreateAsync(TEntity e);
        void Update(TEntity e);
        void Delete(TEntity e);

    }
}
