namespace Asumet.Doc.Office
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Asumet.Doc.Common;
    using NPOI.XWPF.UserModel;

    /// <summary>
    /// Base class for all Word document exporters.
    /// </summary>
    /// <typeparam name="T">Type of the exported object.</typeparam>
    public abstract class WordExporterBase<T> : IOfficeExporter<T>
        where T : class
    {
        private const string DataSetPrefix = "DataSet:";
        private const string DataSetItemPrefix = "DataSetItem:";

        /// <summary>Constructor</summary>
        public WordExporterBase(IAppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        /// <inheritdoc/>
        public virtual string TemplateFileName
        {
            get
            {
                return Path.ChangeExtension(DocumentName, AppSettings.WordTemplateExtension);
            }
        }

        /// <inheritdoc/>
        public virtual bool LeaveMissingPlaceholders { get; set; } = true;

        /// <summary>
        /// Gets the output document file path.
        /// </summary>
        /// <returns>Returns the output document file path.</returns>
        protected virtual string GetOutputFilePath()
        {
            var templateFileName = TemplateFileName;
            string name = Path.GetFileNameWithoutExtension(templateFileName);
            string extension = Path.GetExtension(templateFileName);
            string outputFileName = $"{name}-{DateTime.Now:yyyyMMdd-HHmmss-fffffff}{extension}";
            var result = Path.Combine(AppSettings.DocumentOutputDirectory, outputFileName);
            
            return result;
        }

        /// <summary> Gets the document name. </summary>
        protected abstract string DocumentName { get; }
        
        /// <summary>Application Settings</summary>
        public IAppSettings AppSettings { get; }

        /// <inheritdoc/>
        public string Export(T documentObject)
        {
            ArgumentNullException.ThrowIfNull(nameof(documentObject));


            BeforeExport(documentObject);

            var outputFilePath = GetOutputFilePath();
            CreateOutputDirectoryIfNotExists(outputFilePath);

            using (var rs = File.OpenRead(GetTemplateFilePath()))
            {
                using var doc = new XWPFDocument(rs);
                foreach (var paragraph in doc.Paragraphs)
                {
                    FillParagraph(paragraph, documentObject);
                }

                foreach (var table in doc.Tables.Where(t => t.NumberOfRows > 0))
                {
                    ProcessTable(table, documentObject);
                }

                using var ws = File.Create(outputFilePath);
                doc.Write(ws);
            }

            AfterExport(documentObject);

            return outputFilePath;
        }

        /// <summary>
        /// Creates output directory if not exists
        /// </summary>
        protected void CreateOutputDirectoryIfNotExists(string outputFilePath)
        {
            var outputDirName = Path.GetDirectoryName(outputFilePath);
            if (!Directory.Exists(outputDirName) && outputDirName != null)
            {
                Directory.CreateDirectory(outputDirName);
            }
        }

        /// <summary>
        /// Replaces placeholderNames in <paramref name="paragraph"/> with values from <paramref name="obj"/>
        /// </summary>
        /// <param name="paragraph">Paragraph to process.</param>
        /// <param name="documentObject">Top level parent object with values.</param>
        /// <param name="obj">Object with values. If null then takes values from <paramref name="documentObject"/></param>
        protected void FillParagraph(XWPFParagraph? paragraph, T documentObject, object? obj = null)
        {
            if (paragraph == null)
            {
                return;
            }
            ArgumentNullException.ThrowIfNull(nameof(documentObject));

            obj ??= documentObject;

            var placeholderNames = DocHelper.GetPlaceholderNames(paragraph.ParagraphText);
            foreach (var placeholderName in placeholderNames)
            {
                var memberName = placeholderName;
                var isDatasetItem = memberName.StartsWith(DataSetItemPrefix);
                if (isDatasetItem)
                {
                    memberName = placeholderName[DataSetItemPrefix.Length..];
                }

                var value = DocHelper.GetMemberValue(obj, memberName);
                if (value == null && isDatasetItem && obj != documentObject)
                {
                    // If we didn't find a value in child object, let's try to get it from the parent
                    value = DocHelper.GetMemberValue(documentObject, memberName);
                }

                string? stringValue = string.Empty;
                bool skipReplace = LeaveMissingPlaceholders;
                if (value != null)
                {
                    stringValue = value.ToString();
                    skipReplace = false;
                }

                if (!skipReplace)
                {
                    paragraph.ReplaceText(DocHelper.MakePlaceholder(placeholderName), stringValue);
                }
            }
        }

        /// <summary>
        /// If the first firstCell in the table starts with <see cref="DataSetPrefix"/>:
        ///   1. Add rows to the table
        ///   2. Fill the cell values from the enumberable property of <paramref name="documentObject"/>
        /// Else
        ///   Fill the cell values from <paramref name="documentObject"/>
        /// </summary>
        /// <param name="table">A table to be processed</param>
        /// <param name="documentObject">An object to fill values from</param>
        protected void ProcessTable(XWPFTable table, T documentObject)
        {
            ArgumentNullException.ThrowIfNull(nameof(table));
            ArgumentNullException.ThrowIfNull(nameof(documentObject));

            int rowIndex = 0;
            while (rowIndex < table.Rows.Count)
            {
                var row = table.GetRow(rowIndex);
                int rowCountAdded = ProcessDatasetRow(row, documentObject);
                if (rowCountAdded == 0)
                {
                    FillRow(row, documentObject, documentObject);
                }

                rowIndex += rowCountAdded + 1;
            }
        }

        /// <summary>
        /// Checks if the <paramref name="row"/> is "DataSet" row.
        /// If so, clones this row and fills it from dataset taken from <paramref name="documentObject"/>
        /// </summary>
        /// <param name="row">Row clone if it's a "DataSet" row</param>
        /// <param name="documentObject">An object to take values from</param>
        /// <returns>Row count added to the table</returns>
        protected int ProcessDatasetRow(XWPFTableRow? row, T documentObject)
        {
            if (row == null)
            {
                return 0;
            }
            ArgumentNullException.ThrowIfNull(nameof(documentObject));

            var firstCell = row.GetCell(0);
            var paragraph = firstCell.Paragraphs[0];
            if (paragraph == null)
            {
                return 0;
            }

            var placeholderNames = DocHelper.GetPlaceholderNames(paragraph.ParagraphText);
            var tablePlaceholder = placeholderNames.FirstOrDefault();
            if (tablePlaceholder == null || !tablePlaceholder.StartsWith(DataSetPrefix))
            {
                return 0;
            }

            var datasetMemberName = tablePlaceholder[DataSetPrefix.Length..];
            if (DocHelper.GetMemberValue(documentObject, datasetMemberName) is not IEnumerable<object> items || !items.Any())
            {
                return 0;
            }

            paragraph.ReplaceText(DocHelper.MakePlaceholder($"{DataSetPrefix}{datasetMemberName}"), string.Empty);

            int rowCountToAdd = items.Count() - 1;
            WordWrapper.CloneRow(row, rowCountToAdd);

            FillDatasetRows(row, documentObject, datasetMemberName);

            return rowCountToAdd;
        }

        /// <summary>
        /// Fills cells starting from <paramref name="startRow"/> taking values from <paramref name="datasetMemberName"/>
        /// </summary>
        /// <param name="startRow">Starting row of the table</param>
        /// <param name="documentObject">An object to fill values from</param>
        /// <param name="datasetMemberName">Name of the dataset from <paramref name="documentObject"/></param>
        protected void FillDatasetRows(XWPFTableRow startRow, T documentObject, string datasetMemberName)
        {
            ArgumentNullException.ThrowIfNull(nameof(startRow));
            ArgumentNullException.ThrowIfNull(nameof(documentObject));
            
            var table = startRow.GetTable();
            var rowIndex = table.Rows.IndexOf(startRow);
            if (DocHelper.GetMemberValue(documentObject, datasetMemberName) is not IEnumerable<object> items)
            {
                return;
            }

            foreach (var item in items)
            {
                var row = table.GetRow(rowIndex);
                FillRow(row, documentObject, item);
                rowIndex++;
            }
        }

        /// <summary>
        /// Fills cells in <paramref name="row"/> with values from <paramref name="obj"/>
        /// </summary>
        /// <param name="row">Row to process</param>
        /// <param name="documentObject">A top level parent object for <paramref name="obj"/></param>
        /// <param name="obj">Object to take values from</param>
        protected void FillRow(XWPFTableRow? row, T documentObject, object obj)
        {
            if (row == null)
            {
                return;
            }
            ArgumentNullException.ThrowIfNull(nameof(documentObject));

            var cells = row.GetTableCells();
            foreach (var cell in cells)
            {
                var paragraph = cell.Paragraphs[0];
                if (paragraph == null)
                {
                    continue;
                }

                FillParagraph(paragraph, documentObject, obj);
            }
        }

        /// <summary>
        /// Gets the full path to the document template file.
        /// </summary>
        /// <returns>The full path to the document template file.</returns>
        protected virtual string GetTemplateFilePath()
        {
            return Path.Combine(AppSettings.TemplatesDirectory, TemplateFileName);
        }

        /// <summary>
        /// The method is called before export to a document
        /// </summary>
        protected virtual void BeforeExport(T documentObject)
        {
        }

        /// <summary>
        /// The method is called after export to a document
        /// </summary>
        protected virtual void AfterExport(T documentObject)
        {
        }
    }
}
