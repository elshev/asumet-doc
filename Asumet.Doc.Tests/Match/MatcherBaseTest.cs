using Asumet.Doc.Match;
using Asumet.Entities;

namespace Asumet.Doc.Tests.Match
{
    public class MatcherBaseTest
    {
        private static PsaMatcher CreatePsaMatcher()
        {
            var psa = PsaSeedData.GetPsa(1);
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            return new PsaMatcher(matchPattern);
        }
    }
}
