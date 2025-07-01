using System.Linq.Expressions;

namespace LibraryManagementCleanArchitecture.Application.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<T?> GetByIdAsync(object id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
