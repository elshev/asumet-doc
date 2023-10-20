namespace Asumet.Doc.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Asumet.Common;

    /// <summary>
    /// Contains helper methods to work with Documents (Office, OCR).
    /// </summary>
    public class DocHelper
    {
        /// <summary> "ПСА" Document name /// </summary>
        public const string PsaDocumentName = "ПСА";

        /// <summary>
        /// Gets all placeholders inside curly brackets. Ex.: {Some.Placeholder}.
        /// </summary>
        /// <param name="str">A string where to find placeholders.</param>
        /// <returns>A collection of values found between brackets.</returns>
        public static IEnumerable<string> GetPlaceholderNames(string str)
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

        /// <summary>
        /// Wraps <paramref name="placeholderName"/> into placeholder brackets.
        /// Ex.: "SomeValue" => "{SomeValue}"
        /// </summary>
        /// <param name="placeholderName">Value to make placeholder from</param>
        /// <returns>A placeholder that is made from <paramref name="placeholderName"/></returns>
        /// <remarks>
        /// The goal of this method is to localize placeholder making process in one place, in case
        /// it will be decided to change '{placeholderName}' to something else like '[[placeholderName]]'
        /// </remarks>
        public static string MakePlaceholder(string placeholderName)
        {
            return $"{{{placeholderName}}}";
        }

        /// <summary>
        /// Gets the value of the member with the specified name.
        /// </summary>
        /// <param name="sourceObject">An object instance.</param>
        /// <param name="memberName">The member name. Could be separated by "." to access internal members: "ParentProperty.ChildProperty.SomeMember"</param>
        /// <returns>The member value.</returns>
        /// <remarks>
        /// This method uses Reflection to get the value from object.
        /// For speed optimization the next approaches can be investigated:
        /// 1. Expression trees
        /// 2. Serialize to JSON and work with JSON objects
        /// </remarks>
        public static object GetMemberValue(object sourceObject, string memberName)
        {
            return ReflectionHelper.GetMemberValue(sourceObject, memberName);
        }

        /// <summary>
        /// Replaces all placeholders in <paramref name="str"/> with property values from <paramref name="obj"/>.
        /// </summary>
        /// <param name="str">A string to process.</param>
        /// <param name="obj">Object to take values from.</param>
        /// <param name="skipMissingPlaceholders">
        /// If true - leave a "{placeholderName}" in the output document.
        /// If false - replace it with the empty string.
        /// </param>
        /// <returns>A string with replaced values.</returns>
        public static string ReplacePlaceholdersInString(string str, object obj, bool skipMissingPlaceholders)
        {
            if (obj == null)
            {
                return str;
            }

            var placeholderNames = GetPlaceholderNames(str);
            string result = str;
            foreach (var placeholderName in placeholderNames)
            {
                var memberName = placeholderName;
                var value = GetMemberValue(obj, memberName);
                string? stringValue = string.Empty;
                bool skipReplace = skipMissingPlaceholders;
                if (value != null)
                {
                    stringValue = value.ToString();
                    skipReplace = false;
                }

                if (!skipReplace)
                {
                    result = result.Replace(MakePlaceholder(placeholderName), stringValue);
                }
            }

            return result;
        }

        /// <summary>
        /// Replaces all placeholders in <paramref name="strings"/> with property values from <paramref name="obj"/>.
        /// </summary>
        /// <param name="strings">A list of strings to process.</param>
        /// <param name="obj">Object to take values from.</param>
        /// <param name="skipMissingPlaceholders">
        /// If true - leave a "{placeholderName}" in the output document.
        /// If false - replace it with the empty string.
        /// </param>
        /// <returns>
        /// A new list of strings with replaced values.
        /// Note: If obj is null, return the same <paramref name="strings"/>
        /// </returns>
        public static IEnumerable<string> ReplacePlaceholdersInStrings(IEnumerable<string> strings, object obj, bool skipMissingPlaceholders)
        {
            if (obj == null)
            {
                return strings;
            }

            var result = strings.ToList();
            for (var i = 0; i < result.Count; i++)
            {
                result[i] = ReplacePlaceholdersInString(result[i], obj, skipMissingPlaceholders);
            }

            return result;
        }
    }
}
