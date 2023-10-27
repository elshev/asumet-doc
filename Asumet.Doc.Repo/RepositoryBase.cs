namespace Asumet.Doc.Repo
{
    internal abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        public abstract Task<IEnumerable<TEntity>> GetAllAsync();
        public abstract Task<TEntity> GetByIdAsync(TKey id);
        public abstract TEntity? RemoveEntity(TKey id);
        public abstract TEntity? UpdateEntity(TEntity entity);
    }
}