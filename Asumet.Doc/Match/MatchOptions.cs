namespace Asumet.Doc.Match
{
    /// <summary>
    /// Addition options when matching
    /// </summary>
    public class MatchOptions
    {
        /// <summary> If symbols should be ignored when matching </summary>
        public bool IgnoreSymbols { get; set; } = true;

        /// <summary> Symbols to ignore when matching </summary>
        public char[] SymbolsToIgnore { get; set; } = new char[] { '.', ',', ':', ';', '/', '\\', '|' };
    }
}
