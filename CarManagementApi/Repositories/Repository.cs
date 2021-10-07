using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarManagementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarManagementApi.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAll(int pageNumber, int pageSize);
        ValueTask<TEntity> GetById(int id);
        Task<List<TEntity>> Find(Func<TEntity, bool> filter);
        Task Add(TEntity entity);
        Task Remove(int id);
    }

    public class Repository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly DbSet<TEntity> DbEntity;

        public Repository(CarManagementContext context)
        {
            DbEntity = context.Set<TEntity>();
        }

        public virtual Task<List<TEntity>> GetAll(int pageNumber, int pageSize)
        {
            return GetEntities(pageNumber, pageSize).ToListAsync();
        }

        public ValueTask<TEntity> GetById(int id) => DbEntity.FindAsync(id);

        public Task<List<TEntity>> Find(Func<TEntity, bool> filter)
        {
            return Task.FromResult(DbEntity.Where(filter).ToList());
        }

        public async Task Add(TEntity entity)
        {
            await DbEntity.AddAsync(entity).ConfigureAwait(false);
        }

        public Task Remove(int id)
        {
            DbEntity.Remove(new TEntity { Id = id });

            return Task.CompletedTask;
        }

        protected IQueryable<TEntity> GetEntities(int pageNumber, int pageSize)
        {
            return DbEntity.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
