namespace Asumet.Doc.Match
{
    using System;

    /// <summary>
    /// Addition options when matching
    /// </summary>
    public class MatchOptions
    {
        /// <summary> Ignore case when matching </summary>
        public bool IgnoreCase { get; set; } = true;

        /// <summary> Symbols to ignore when matching </summary>
        public string[] SymbolsToIgnore { get; set; } = Array.Empty<string>();

        /// <summary> Default options </summary>
        public static MatchOptions DefaultOptions()
        {
            return new MatchOptions();
        }

        /// <summary> Options with ignored symbols like comma, dot, colon, etc.</summary>
        public static MatchOptions IgnoreSymbolsOptions()
        {
            return new MatchOptions { SymbolsToIgnore = new string[] { " ", ".", ",", ":", ";", "/", "\\", "|", "_" } };
        }
    }
}
