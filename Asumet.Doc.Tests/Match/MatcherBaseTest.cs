using Asumet.Doc.Match;
using Asumet.Models;

namespace Asumet.Doc.Tests.Match
{
    public class MatcherBaseTest
    {
        private static PsaMatcher CreatePsaMatcher()
        {
            var psa = Psa.GetPsaStub();
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            return new PsaMatcher(matchPattern);
        }
    }
}
