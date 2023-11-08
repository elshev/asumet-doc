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
        public MatchService(
            IRepositoryBase<TEntity, TKey> repository,
            IMatcher<TEntity> matcher)
        {
            Repository = repository;
            Matcher = matcher;
        }

        protected IRepositoryBase<TEntity, TKey> Repository { get; }
        public IMatcher<TEntity> Matcher { get; }

        private static IEnumerable<string> DoOcr(string imageFilePath)
        {
            var lines = OcrWrapper.ImageToStrings(imageFilePath);
            return lines;
        }

        private int Match(TEntity entity, string imageFilePath)
        {
            var lines = DoOcr(imageFilePath);
            var score = Matcher.MatchDocumentWithPattern(lines, entity);
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