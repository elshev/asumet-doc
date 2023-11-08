using Asumet.Doc.Match;
using Asumet.Doc.Repo;
using Asumet.Doc.Services.Match;
using Asumet.Entities;

namespace Asumet.Doc.Services.Psas
{
    public interface IPsaMatchService : IMatchService<Psa, int>
    {
    }

    public class PsaMatchService : MatchService<Psa, int>, IPsaMatchService
    {
        public PsaMatchService(IRepositoryBase<Psa, int> repository)
            : base(repository)
        {
        }

        protected override IMatcher<Psa> CreateMatcher(IMatchPattern<Psa> matchPattern)
        {
            return new PsaMatcher(matchPattern);
        }

        protected override IMatchPattern<Psa> CreateMatchPattern(Psa entity)
        {
            return new PsaMatchPattern(entity);
        }
    }
}