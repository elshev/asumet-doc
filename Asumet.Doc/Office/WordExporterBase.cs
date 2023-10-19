namespace Asumet.Doc.Office
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Asumet.Common;
    using Asumet.Doc.Common;
    using NPOI.XWPF.UserModel;

    /// <summary>
    /// Base class for all document (Word, Excel) exporters.
    /// </summary>
    /// <typeparam name="T">Type of the exported object.</typeparam>
    public abstract class WordExporterBase<T> : IOfficeExporter<T>
        where T : class
    {
        private const string DataSetPrefix = "DataSet:";
        private const string DataSetItemPrefix = "DataSetItem:";

        private string? _outputFilePath;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="objectToExport">Object to export to a document.</param>
        public WordExporterBase(T objectToExport)
        {
            ObjectToExport = objectToExport;
        }

        /// <inheritdoc/>
        public T ObjectToExport { get; }

        /// <inheritdoc/>
        public virtual string OutputFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_outputFilePath))
                {
                    string name = Path.GetFileNameWithoutExtension(TemplateFileName);
                    string extension = Path.GetExtension(TemplateFileName);
                    string outputFileName = $"{name}-{DateTime.Now:yyyyMMdd-HHmmss-fffffff}{extension}";
                    _outputFilePath = Path.Combine(AppSettings.Instance.DocumentOutputDirectory, outputFileName);
                }

                return _outputFilePath;
            }
        }

        /// <summary>
        /// Gets the document template file name.
        /// </summary>
        protected abstract string TemplateFileName { get; }

        /// <summary>
        /// If true - leave a placeholder in the output document
        /// If false - replace it with the empty string
        /// </summary>
        protected virtual bool SkipMissingPlaceholders { get; } = true;

        /// <inheritdoc/>
        public virtual void Export()
        {
            CreatesOutputDirectoryIfNotExists();

            BeforeExport();

            using (var rs = File.OpenRead(GetTemplateFilePath()))
            {
                using var doc = new XWPFDocument(rs);
                foreach (var paragraph in doc.Paragraphs)
                {
                    FillParagraph(paragraph);
                }

                foreach (var table in doc.Tables.Where(t => t.NumberOfRows > 0))
                {
                    ProcessTable(table);
                }

                using var ws = File.Create(OutputFilePath);
                doc.Write(ws);
            }

            AfterExport();
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
        protected static object GetMemberValue(object sourceObject, string memberName)
        {
            return ReflectionHelper.GetMemberValue(sourceObject, memberName);
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
        protected static void CloneRow(XWPFTableRow baseRow, int count)
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

        /// <summary>
        /// Creates output directory if not exists
        /// </summary>
        protected void CreatesOutputDirectoryIfNotExists()
        {
            var outputDirName = Path.GetDirectoryName(OutputFilePath);
            if (!Directory.Exists(outputDirName) && outputDirName != null)
            {
                Directory.CreateDirectory(outputDirName);
            }
        }

        /// <summary>
        /// Replaces placeholders in <paramref name="paragraph"/> with values from <paramref name="obj"/>
        /// </summary>
        /// <param name="paragraph">Paragraph to process.</param>
        /// <param name="obj">Object with values. If null then takes values from <see cref="ObjectToExport"/></param>
        protected void FillParagraph(XWPFParagraph? paragraph, object? obj = null)
        {
            if (paragraph == null)
            {
                return;
            }

            obj ??= ObjectToExport;

            var placeholders = DocHelper.GetPlaceholders(paragraph.ParagraphText);
            foreach (var placeholder in placeholders)
            {
                var memberName = placeholder;
                var isDatasetItem = memberName.StartsWith(DataSetItemPrefix);
                if (isDatasetItem)
                {
                    memberName = placeholder[DataSetItemPrefix.Length..];
                }

                var value = GetMemberValue(obj, memberName);
                if (value == null && isDatasetItem && obj != ObjectToExport)
                {
                    // If we didn't find a value in child object, let's try to get it from the parent
                    value = GetMemberValue(ObjectToExport, memberName);
                }

                string? stringValue = string.Empty;
                bool skipReplace = SkipMissingPlaceholders;
                if (value != null)
                {
                    stringValue = value.ToString();
                    skipReplace = false;
                }

                if (!skipReplace)
                {
                    paragraph.ReplaceText($"{{{placeholder}}}", stringValue);
                }
            }
        }

        /// <summary>
        /// If the first firstCell in the table starts with <see cref="DataSetPrefix"/>:
        ///   1. Add rows to the table
        ///   2. Fill the cell values from the enumberable property of <see cref="ObjectToExport"/>
        /// Else
        ///   Fill the cell values from <see cref="ObjectToExport"/>
        /// </summary>
        /// <param name="table">A table to be processed</param>
        protected void ProcessTable(XWPFTable table)
        {
            int rowIndex = 0;
            while (rowIndex < table.Rows.Count)
            {
                var row = table.GetRow(rowIndex);
                int rowCountAdded = ProcessDatasetRow(row);
                if (rowCountAdded == 0)
                {
                    FillRow(row, ObjectToExport);
                }

                rowIndex += rowCountAdded + 1;
            }
        }

        /// <summary>
        /// Checks if the <paramref name="row"/> is "DataSet" row.
        /// If so, clones this row and fills it from dataset taken from <see cref="ObjectToExport"/>
        /// </summary>
        /// <param name="row">Row clone if it's a "DataSet" row</param>
        /// <returns>Row count added to the table</returns>
        protected int ProcessDatasetRow(XWPFTableRow? row)
        {
            if (row == null)
            {
                return 0;
            }

            var firstCell = row.GetCell(0);
            var paragraph = firstCell.Paragraphs[0];
            if (paragraph == null)
            {
                return 0;
            }

            var placeholders = DocHelper.GetPlaceholders(paragraph.ParagraphText);
            var tablePlaceholder = placeholders.FirstOrDefault();
            if (tablePlaceholder == null || !tablePlaceholder.StartsWith(DataSetPrefix))
            {
                return 0;
            }

            var datasetMemberName = tablePlaceholder[DataSetPrefix.Length..];
            if (GetMemberValue(ObjectToExport, datasetMemberName) is not IEnumerable<object> items || !items.Any())
            {
                return 0;
            }

            paragraph.ReplaceText($"{{{DataSetPrefix}{datasetMemberName}}}", string.Empty);

            int rowCountToAdd = items.Count() - 1;
            CloneRow(row, rowCountToAdd);

            FillDatasetRows(row, datasetMemberName);

            return rowCountToAdd;
        }

        /// <summary>
        /// Fills cells starting from <paramref name="startRow"/> taking values from <paramref name="datasetMemberName"/>
        /// </summary>
        /// <param name="startRow">Starting row of the table</param>
        /// <param name="datasetMemberName">Name of the dataset from <see cref="ObjectToExport"/></param>
        protected void FillDatasetRows(XWPFTableRow startRow, string datasetMemberName)
        {
            var table = startRow.GetTable();
            var rowIndex = table.Rows.IndexOf(startRow);
            if (GetMemberValue(ObjectToExport, datasetMemberName) is not IEnumerable<object> items)
            {
                return;
            }

            foreach (var item in items)
            {
                var row = table.GetRow(rowIndex);
                FillRow(row, item);
                rowIndex++;
            }
        }

        /// <summary>
        /// Fills cells in <paramref name="row"/> with values from <paramref name="obj"/>
        /// </summary>
        /// <param name="row">Row to process</param>
        /// <param name="obj">Object to take values from</param>
        protected void FillRow(XWPFTableRow? row, object obj)
        {
            if (row == null)
            {
                return;
            }

            var cells = row.GetTableCells();
            foreach (var cell in cells)
            {
                var paragraph = cell.Paragraphs[0];
                if (paragraph == null)
                {
                    continue;
                }

                FillParagraph(paragraph, obj);
            }
        }

        /// <summary>
        /// Gets the full path to the document template file.
        /// </summary>
        /// <returns>The full path to the document template file.</returns>
        protected virtual string GetTemplateFilePath()
        {
            return Path.Combine(AppSettings.Instance.TemplatesDirectory, TemplateFileName);
        }

        /// <summary>
        /// The method is called before export to a document
        /// </summary>
        protected virtual void BeforeExport()
        {
        }

        /// <summary>
        /// The method is called after export to a document
        /// </summary>
        protected virtual void AfterExport()
        {
        }
    }
}
