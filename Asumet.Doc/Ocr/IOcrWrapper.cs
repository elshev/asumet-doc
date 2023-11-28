
namespace Asumet.Doc.Ocr
{
    /// <summary>
    /// Wraps Tesseract OCR
    /// </summary>
    public interface IOcrWrapper
    {
        /// <summary>
        /// Processes the image and returns lines of the parsed text
        /// </summary>
        /// <param name="imageFilePath">Path to an image file</param>
        /// <returns>A parsed image as a list of strings</returns>
        IEnumerable<string> ImageToStrings(string imageFilePath);
    }
}