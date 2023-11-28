using Asumet.Entities;

namespace Asumet.Doc.Repo
{
    public interface IRepositoryBase<TEntity, TKey>
        where TKey : unmanaged
        where TEntity : EntityBase<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        Task<TEntity?> GetByIdAsync(TKey id);

        Task<TEntity?> InsertEntityAsync(TEntity entity);

        Task<TEntity?> UpdateEntityAsync(TEntity entity);

        Task<TEntity?> RemoveEntityAsync(TKey id);

    }
}