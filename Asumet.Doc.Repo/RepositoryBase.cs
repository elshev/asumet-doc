using Asumet.Entities;
using Microsoft.EntityFrameworkCore;

namespace Asumet.Doc.Repo
{
    internal abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : EntityBase<TKey>
    {
        protected RepositoryBase(DocDbContext docDb)
        {
            DocDb = docDb;
        }

        protected DocDbContext DocDb { get; }

        protected abstract DbSet<TEntity> DbSet { get; }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual TEntity? GetById(TKey id)
        {
            var task = GetByIdAsync(id);
            task.Wait();
            return task.Result;
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            var result = await DbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));
            return result;
        }

        public virtual async Task<TEntity?> InsertEntityAsync(TEntity entity)
        {
            if (!entity.Id.Equals(default(TKey)))
            {
                var existingEntity = await GetByIdAsync(entity.Id);
                if (existingEntity != null)
                {
                    return null;
                }
            }

            await DbSet.AddAsync(entity);
            await DocDb.SaveChangesAsync();
            return entity;
        }

        public virtual TEntity? RemoveEntity(TKey id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return null;
            }

            DbSet.Remove(entity);
            DocDb.SaveChanges();
            return entity;
        }

        public virtual TEntity? UpdateEntity(TEntity entity)
        {
            var existingEntity = GetById(entity.Id);
            if (existingEntity == null)
            {
                return null;
            }

            DocDb.Entry(existingEntity).CurrentValues.SetValues(entity);
            DocDb.SaveChanges();
            return existingEntity;
        }
    }
}