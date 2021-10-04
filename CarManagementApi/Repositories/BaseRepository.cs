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

    public class BaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly DbSet<TEntity> dbEntity;

        public BaseRepository(CarManagementContext context)
        {
            dbEntity = context.Set<TEntity>();
        }

        public virtual Task<List<TEntity>> GetAll(int pageNumber, int pageSize)
        {
            return GetEntities(pageNumber, pageSize).ToListAsync();
        }

        public ValueTask<TEntity> GetById(int id) => dbEntity.FindAsync(id);

        public Task<List<TEntity>> Find(Func<TEntity, bool> filter)
        {
            return Task.FromResult(dbEntity.Where(filter).ToList());
        }

        public async Task Add(TEntity entity)
        {
            await dbEntity.AddAsync(entity).ConfigureAwait(false);
        }

        public Task Remove(int id)
        {
            dbEntity.Remove(new TEntity { Id = id });

            return Task.CompletedTask;
        }

        protected IQueryable<TEntity> GetEntities(int pageNumber, int pageSize)
        {
            return dbEntity.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
