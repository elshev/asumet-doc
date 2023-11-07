using Asumet.Entities;

namespace Asumet.Doc.Repo
{
    public interface IRepositoryBase<TEntity, TKey>
        where TKey : unmanaged
        where TEntity : EntityBase<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        TEntity? GetById(TKey id);

        Task<TEntity?> GetByIdAsync(TKey id);

        Task<TEntity?> InsertEntityAsync(TEntity entity);

        TEntity? UpdateEntity(TEntity entity);

        TEntity? RemoveEntity(TKey id);

    }
}