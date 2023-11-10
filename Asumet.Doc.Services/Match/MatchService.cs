using Asumet.Doc.Match;
using Asumet.Doc.Repo;
using Asumet.Entities;

namespace Asumet.Doc.Services.Match
{
    public abstract class MatchService<TEntity, TKey> : DocServiceBase, IMatchService<TEntity, TKey>
        where TKey : unmanaged
        where TEntity : EntityBase<TKey>

    {
        public MatchService(
            IRepositoryBase<TEntity, TKey> repository,
            IMatcher<TEntity> matcher)
        {
            Repository = repository;
            Matcher = matcher;
        }

        protected IRepositoryBase<TEntity, TKey> Repository { get; }
        public IMatcher<TEntity> Matcher { get; }

        public async Task<int> MatchAsync(TKey id, string imageFilePath)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null)
            {
                return 0;
            }

            var result = await Matcher.MatchDocumentImageWithPatternAsync(imageFilePath, entity);
            return result;
        }
    }
}