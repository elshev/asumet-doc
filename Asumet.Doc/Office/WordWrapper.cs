using Asumet.Common;
using NPOI.XWPF.UserModel;
using System.Text;

namespace Asumet.Doc.Office
{
    /// <summary>Options for conversion Word -> Text</summary>
    public struct WordFileToTextOptions
    {
        /// <summary>Table header rows to skip when processing a table</summary>
        public int SkipFirstTableRowCount { get; set; }

        /// <summary>Table summary rows to skip when processing a table</summary>
        public int SkipLastTableRowCount { get; set; }
    }

    /// <summary>
    /// Contains methods to work with .docx file via 3rd party libraries like NPOI
    /// </summary>
    public static class WordWrapper
    {
        /// <summary>Table separator when exporting to text</summary>
        public const string TextTableSeparator = "| ";

        /// <summary>
        /// Extracts text from MS Word file and saves it to <paramref name="outputTextFilePath"/>
        /// </summary>
        /// <param name="wordFilePath">Path to MS Word file to process</param>
        /// <param name="outputTextFilePath">Path to the output text file</param>
        /// <param name="options">Conversion options</param>
        public static void WordFileToTextFile(string wordFilePath, string outputTextFilePath, WordFileToTextOptions options)
        {
            ArgumentNullException.ThrowIfNull(nameof(wordFilePath));
            ArgumentNullException.ThrowIfNull(nameof(outputTextFilePath));
            
            using var fileStream = File.OpenRead(wordFilePath);
            using var doc = new XWPFDocument(fileStream);
            var result = WordFileToText(doc, options);
            PathHelper.CreatePathDirectoryIfNotExists(outputTextFilePath);
            if (File.Exists(outputTextFilePath))
            {
                File.Delete(outputTextFilePath);
            }
            
            File.AppendAllLines(outputTextFilePath, result);
        }

        /// <summary>
        /// Gets text from MS Word file
        /// </summary>
        /// <param name="wordFilePath">MS Word file to process</param>
        /// <param name="options">Conversion options</param>
        /// <returns>Lines of extracted text</returns>
        public static IEnumerable<string> WordFileToText(string wordFilePath, WordFileToTextOptions options)
        {
            using var fileStream = File.OpenRead(wordFilePath);
            using var doc = new XWPFDocument(fileStream);
            var result = WordFileToText(doc, options);
            return result;
        }

        /// <summary>
        /// Clones a row <paramref name="count"/> times after the <paramref name="baseRow"/>
        /// </summary>
        /// <param name="baseRow">A row to clone.</param>
        /// <param name="count">Row count to add.</param>
        /// <remarks>
        /// There was no working way found to clone a row to the specified index.
        /// See: https://stackoverflow.com/questions/77311344/npoi-word-how-to-clone-row-and-insert-it-at-the-specified-index
        /// Workaround:
        /// 1. <paramref name="baseRow"/> is cloned to the end of the table.
        /// 2. All remaining rows below it are cloned as well to be append to the end
        /// 3. Then these all remaining rows below it are removed.
        /// </remarks>
        internal static void CloneRow(XWPFTableRow baseRow, int count)
        {
            var table = baseRow.GetTable();
            var baseIndex = table.Rows.IndexOf(baseRow);
            var lastRows = table.Rows.GetRange(baseIndex + 1, table.Rows.Count - (baseIndex + 1));
            for (int i = 0; i < count; i++)
            {
                XWPFTableRow newRow = baseRow.CloneRow();
            }

            lastRows.ForEach(row =>
            {
                row.CloneRow();
                table.RemoveRow(baseIndex + 1);
            });
        }
        
        private static IEnumerable<string> WordFileToText(XWPFDocument doc, WordFileToTextOptions options)
        {
            var result = new List<string>();
            foreach (var bodyElement in doc.BodyElements)
            {
                if (bodyElement is XWPFParagraph paragraph)
                {
                    result.Add(paragraph.Text);
                    continue;
                }

                if (bodyElement is not XWPFTable table)
                {                    
                    continue;
                }

                
                for (int rowIndex = options.SkipFirstTableRowCount;
                         rowIndex < table.Rows.Count - options.SkipLastTableRowCount;
                         rowIndex++)
                {
                    var row = table.Rows[rowIndex];
                    var tableLine = new StringBuilder();
                    foreach (var cell in row.GetTableCells())
                    {
                        foreach (var cellParagraph in cell.Paragraphs)
                        {
                            tableLine.Append(cellParagraph.Text);
                            tableLine.Append(TextTableSeparator);
                        }
                    }

                    result.Add(tableLine.ToString());
                }
            }

            return result;
        }
    }
}
