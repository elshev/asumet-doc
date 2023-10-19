namespace Asumet.Doc.Ocr
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Wraps Ocr
    /// </summary>
    public class OcrWrapper
    {
        /// <summary>
        /// Ocr
        /// </summary>
        /// <param name="imageFilePath">Path to an image file</param>
        /// <returns>Lit of strings</returns>
        public IEnumerable<string> OcrImage(string imageFilePath)
        {
            IList<string> result = new List<string>();

            return result;
        }
    }
}
