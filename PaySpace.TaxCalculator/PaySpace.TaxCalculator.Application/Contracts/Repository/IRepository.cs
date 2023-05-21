using System.Linq.Expressions;

namespace PaySpace.TaxCalculator.Application.Contracts.Repository
{
    public interface IRepository<TKey, TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(TKey entity);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAsync();
        IEnumerable<TEntity> Get();
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
