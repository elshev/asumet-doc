using Asumet.Entities;

namespace Asumet.Doc.Services.Match
{
    public interface IMatchService<TEntity, TKey> : IDocServiceBase
        where TKey : unmanaged
        where TEntity : EntityBase<TKey>
    {
        /// <summary>
        /// Matches Psa with scanned image
        /// </summary>
        /// <param name="entityId">Entity Id</param>
        /// <param name="imageFilePath">Path to the scanned image file</param>
        /// <returns>A score of matching (0% - 100%)</returns>
        Task<int> MatchAsync(TKey entityId, string imageFilePath);
    }
}
