namespace Asumet.Doc.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ApiControllerBase : ControllerBase
    {
        /// <summary>Content-Type for .docx</summary>
        public const string DocxContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        /// <summary>Extension for Word doc files</summary>
        public const string DocxExtension = ".docx";

        /// <summary>
        /// Creates an IActionResult for .docx file
        /// </summary>
        /// <param name="docxFilePath">A path to .docx file</param>
        /// <returns>IActionResult for a file</returns>
        protected IActionResult GetDocxFileResult(string docxFilePath)
        {

            if (string.IsNullOrEmpty(docxFilePath))
            {
                return BadRequest("Failed to generate the Word document.");
            }

            string fileName = Path.GetFileName(docxFilePath);

            return PhysicalFile(docxFilePath, DocxContentType, fileName);
        }

        /// <summary>
        /// Creates an IActionResult for .docx stream
        /// </summary>
        /// <param name="stream">A stream with .docx file</param>
        /// <param name="name">Name for .docx file</param>
        /// <returns>IActionResult for a file</returns>
        protected IActionResult GetDocxFileResult(Stream stream, string name)
        {
            string fileName = $"{name}-{DateTime.Now:yyyyMMdd-HHmmss-fffffff}{DocxExtension}";
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, DocxContentType)
            {
                FileDownloadName = fileName
            };
        }

    }
}
