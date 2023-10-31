using Asumet.Doc.Match;
using Asumet.Doc.Ocr;
using Asumet.Doc.Repo;
using Asumet.Entities;

namespace Asumet.Doc.Services
{
    public class MatchService : DocServiceBase, IMatchService
    {
        public MatchService(IPsaRepository psaRepository)
        {
            PsaRepository = psaRepository;
        }

        protected IPsaRepository PsaRepository { get; }


        private static IEnumerable<string> DoOcr(string imageFilePath)
        {
            var lines = OcrWrapper.ImageToStrings(imageFilePath);
            return lines;
        }

        private static int Match(Psa psa, string imageFilePath)
        {
            var lines = DoOcr(imageFilePath);
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var matcher = new PsaMatcher(matchPattern);
            var score = matcher.MatchDocumentWithPattern(lines);

            return score;
        }

        public async Task<int> MatchPsaAsync(int psaId, string imageFilePath)
        {
            var psa = await PsaRepository.GetByIdAsync(psaId);
            if (psa == null)
            {
                return 0;
            }

            var result = Match(psa, imageFilePath);
            return result;
       }
    }
}