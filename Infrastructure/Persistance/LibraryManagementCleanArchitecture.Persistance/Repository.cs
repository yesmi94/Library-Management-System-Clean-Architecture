// <copyright file="Repository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Persistance
{
    using System.Linq.Expressions;
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class Repository<T> : IRepository<T>
        where T : class

    {
        protected readonly DataContext context;
        private readonly DbSet<T> entities;

        public Repository(DataContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(object id) => await this.entities.FindAsync(id);

        public async Task<List<T>> GetAllAsync() => await this.entities.ToListAsync();

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await this.entities.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await this.entities.AddAsync(entity);

        public Task<LoginInfo?> GetByUsernameAsync(string username) => this.context.Logins.FirstOrDefaultAsync(info => info.Username == username);

        public Task DeleteAsync(T entity)
        {
            this.entities.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            this.entities.Update(entity);
            return Task.CompletedTask;
        }
    }
}
