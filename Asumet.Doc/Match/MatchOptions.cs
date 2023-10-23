namespace Asumet.Doc.Match
{
    using System;

    /// <summary>
    /// Addition options when matching
    /// </summary>
    public class MatchOptions
    {
        /// <summary> Default options </summary>
        public static MatchOptions DefaultOptions { get; } = new MatchOptions();

        /// <summary> Options with ignored symbols like comma, dot, colon, etc.</summary>
        public static MatchOptions IgnoreSymbolsOptions { get; } = new MatchOptions
        {
            SymbolsToIgnore = new char[] { '.', ',', ':', ';', '/', '\\', '|' }
        };

        /// <summary> Ignore case when matching </summary>
        public bool IgnoreCase { get; set; } = true;

        /// <summary> Symbols to ignore when matching </summary>
        public char[] SymbolsToIgnore { get; set; } = Array.Empty<char>();
    }
}
