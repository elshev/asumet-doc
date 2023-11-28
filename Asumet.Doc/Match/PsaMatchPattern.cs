namespace Asumet.Doc.Match
{
    using Asumet.Doc;
    using Asumet.Doc.Common;
    using Asumet.Entities;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaMatchPattern : MatchPatternBase<Psa>
    {
        /// <inheritdoc/>
        public PsaMatchPattern(IAppSettings appSettings)
            : base(appSettings)
        {
        }

        /// <inheritdoc/>
        protected override string DocumentName => DocHelper.PsaDocumentName;
    }
}
