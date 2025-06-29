using LibraryManagementCleanArchitecture.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementCleanArchitecture.Persistance
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext context;
        private readonly DbSet<T> entities;

        public Repository(DataContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(object id) => await entities.FindAsync(id);

        public async Task<List<T>> GetAllAsync() => await entities.ToListAsync();

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await entities.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await entities.AddAsync(entity);

        public Task DeleteAsync(T entity)
        {
            entities.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            entities.Update(entity);
            return Task.CompletedTask;
        }
    }

}
