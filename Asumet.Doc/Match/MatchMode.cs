namespace Asumet.Doc.Match
{
    /// <summary>
    /// How to compare a recognized document
    /// </summary>
    public enum MatchMode
    {
        /// <summary>
        /// Object -> [PatternFileName].txt -> Compare
        /// Export an object first to a corresponding pattern and then compare with it.
        /// </summary>
        Pattern,

        /// <summary>
        /// Object -> Word -> Text -> Compare
        /// Export an object first to a Word file, then from Word to '.txt' file, and then compare with it.
        /// </summary>
        Document
    }
}
