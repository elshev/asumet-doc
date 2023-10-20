namespace Asumet.Doc.Ocr
{
    using System;
    using System.Collections.Generic;
    using Tesseract;

    /// <summary>
    /// Wraps Tesseract OCR
    /// </summary>
    public static class OcrWrapper
    {
        private static TesseractEngine Engine { get; } = new TesseractEngine(AppSettings.Instance.TesseractDataDirectory, "rus", EngineMode.Default);

        /// <summary>
        /// Processes the image and returns lines of the parsed text
        /// </summary>
        /// <param name="imageFilePath">Path to an image file</param>
        /// <returns>A parsed image as a list of strings</returns>
        public static IEnumerable<string> ImageToStrings(string imageFilePath)
        {
            using var page = ImageToPage(imageFilePath);
            var text = page.GetText();
            var result = text.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None);
            return result;
        }

        /// <summary>
        /// Parses an image from <paramref name="imageFilePath"/> and returns <see cref="Tesseract.Page"/>
        /// </summary>
        /// <param name="imageFilePath">Path to an image file</param>
        /// <returns>A parsed page</returns>
        private static Page ImageToPage(string imageFilePath)
        {
            using var img = Pix.LoadFromFile(imageFilePath);
            var page = Engine.Process(img);
            return page;
        }
    }
}
