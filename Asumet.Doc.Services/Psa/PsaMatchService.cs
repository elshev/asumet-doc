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
        public PsaMatchService(IRepositoryBase<Psa, int> repository, IMatcher<Psa> matcher) 
            : base(repository, matcher)
        {
        }
    }
}