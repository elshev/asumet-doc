using Asumet.Doc.Match;
using Asumet.Doc.Ocr;
using Asumet.Doc.Repo;
using Asumet.Entities;

namespace Asumet.Doc.Services.Match
{
    public abstract class MatchService<TEntity, TKey> : DocServiceBase, IMatchService<TEntity, TKey>
        where TKey : unmanaged
        where TEntity : EntityBase<TKey>

    {
        public MatchService(IRepositoryBase<TEntity, TKey> repository)
        {
            Repository = repository;
        }

        protected IRepositoryBase<TEntity, TKey> Repository { get; }


        protected abstract IMatchPattern<TEntity> CreateMatchPattern(TEntity entity);

        protected abstract IMatcher<TEntity> CreateMatcher(IMatchPattern<TEntity> matchPattern);

        private static IEnumerable<string> DoOcr(string imageFilePath)
        {
            var lines = OcrWrapper.ImageToStrings(imageFilePath);
            return lines;
        }

        private int Match(TEntity entity, string imageFilePath)
        {
            var lines = DoOcr(imageFilePath);
            var matchPattern = CreateMatchPattern(entity);
            var matcher = CreateMatcher(matchPattern);
            var score = matcher.MatchDocumentWithPattern(lines);

            return score;
        }

        public async Task<int> MatchAsync(TKey id, string imageFilePath)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null)
            {
                return 0;
            }

            var result = Match(entity, imageFilePath);
            return result;
        }
    }
}