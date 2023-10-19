namespace Asumet.Doc.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Contains helper methods to work with MS Doc.
    /// </summary>
    public class DocHelper
    {
        /// <summary>
        /// Gets all placeholders inside curly brackets. Ex.: {Some.Placeholder}.
        /// </summary>
        /// <param name="str">A string where to find placeholders.</param>
        /// <returns>A collection of values found between brackets.</returns>
        public static IEnumerable<string> GetPlaceholders(string str)
        {
            var result = new List<string>();
            string pattern = @"\{([^}]+)\}";
            var regex = new Regex(pattern);
            var matches = regex.Matches(str);

            foreach (Match match in matches.Cast<Match>())
            {
                if (match.Groups.Count > 1)
                {
                    string value = match.Groups[1].Value;
                    result.Add(value);
                }
            }

            return result.ToArray();
        }
    }
}
