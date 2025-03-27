using System.Linq.Expressions;

namespace gmltec.application.Contracts.Persistence
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        void Update(T entity);  
        Task<T> GetByIdWithoutExpressionAsync(long id);
        Task<T> GetByIdWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task MarkAsDeletedAsync(Expression<Func<T, bool>> predicate);
    }
}
