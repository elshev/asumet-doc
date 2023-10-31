namespace Asumet.Doc.Services
{
    public interface IMatchService : IDocServiceBase
    {
        /// <summary>
        /// Matches Psa with scanned image
        /// </summary>
        /// <param name="psaId">Psa Id</param>
        /// <param name="imageFilePath">Path to the scanned image file</param>
        /// <returns>A score of matching (0% - 100%)</returns>
        Task<int> MatchPsaAsync(int psaId, string imageFilePath);
    }
}
