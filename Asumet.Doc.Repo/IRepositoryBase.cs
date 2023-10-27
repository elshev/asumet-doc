namespace Asumet.Doc.Repo
{
    public interface IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        Task<TEntity> GetByIdAsync(TKey id);

        TEntity? UpdateEntity(TEntity entity);

        TEntity? RemoveEntity(TKey id);

    }
}