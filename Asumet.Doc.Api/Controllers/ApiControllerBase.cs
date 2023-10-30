namespace Asumet.Doc.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ApiControllerBase : ControllerBase
    {
        /// <summary> Content-Type for .docx </summary>
        public const string DocxContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        /// <summary>
        /// Creates a result for .docx file
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

    }
}
