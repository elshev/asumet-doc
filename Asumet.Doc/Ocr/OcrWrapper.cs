namespace Asumet.Doc.Ocr
{
    using Asumet.Common;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using Tesseract;

    /// <summary>
    /// Wraps Tesseract OCR
    /// </summary>
    public static class OcrWrapper
    {
        /// <summary> Static Constructor </summary>
        static OcrWrapper()
        {
            IsCommandLineMode = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            if (!IsCommandLineMode)
            {
                Engine = new TesseractEngine(AppSettings.Instance.TesseractDataDirectory, "rus", EngineMode.Default);
            }
        }

        /// <summary>
        /// A workaround for Tesseract NuGet package that is not working in Linux
        /// </summary>
        private static bool IsCommandLineMode { get; }

        private static TesseractEngine? Engine { get; }

        /// <summary>
        /// Processes the image and returns lines of the parsed text
        /// </summary>
        /// <param name="imageFilePath">Path to an image file</param>
        /// <returns>A parsed image as a list of strings</returns>
        public static IEnumerable<string> ImageToStrings(string imageFilePath)
        {
            ArgumentNullException.ThrowIfNull(nameof(imageFilePath));
            
            string[] result;
            if (IsCommandLineMode)
            {
                var textFilePath = ImageToTextFile(imageFilePath);
                result = File.ReadAllLines(textFilePath);
                File.Delete(textFilePath);
            }
            else
            {
                using var page = ImageToPage(imageFilePath);
                var text = page.GetText();
                result = text.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None);
            }

            return result;
        }

        private static string ImageToTextFile(string imageFilePath)
        {
            ArgumentNullException.ThrowIfNull(nameof(imageFilePath));

            const string tesseractLanguage = "rus";
            const int ocrTimeout = 30 * 1000;

            var outputFilePath = PathHelper.GetTempFileName("", false);

            var process = new Process();
            process.StartInfo.FileName="tesseract";
            process.StartInfo.Arguments = $"{imageFilePath} {outputFilePath} -l {tesseractLanguage}";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            var sb = new StringBuilder();
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                sb.AppendLine(line);
            }
            var isSuccess = process.WaitForExit(ocrTimeout);
            if (!isSuccess)
            {
                throw new Exception($"Tesseract process timed out ({ocrTimeout/1000}seconds). Output:{Environment.NewLine}{sb}");
            }

            return outputFilePath + ".txt";
        }

        /*        /// <summary>
                /// Do OSD
                /// </summary>
                /// <param name="imageFilePath">Path to an image file</param>
                public static void ImageToOsd(string imageFilePath)
                {
                    using var img = Pix.LoadFromFile(imageFilePath);
                    using var page = Engine.Process(img, PageSegMode.OsdOnly);
                    page.DetectBestOrientation(out int orientation, out float confidence);
                    Console.WriteLine($"Orientation = {orientation}, Confidence = {confidence}");
                }
        */

        /// <summary>
        /// Parses an image from <paramref name="imageFilePath"/> and returns <see cref="Tesseract.Page"/>
        /// </summary>
        /// <param name="imageFilePath">Path to an image file</param>
        /// <returns>A parsed page</returns>
        private static Page ImageToPage(string imageFilePath)
        {
            ArgumentNullException.ThrowIfNull(nameof(imageFilePath));
            if (Engine == null)
            {
                throw new NullReferenceException(nameof(Engine));

            }

            using var img = Pix.LoadFromFile(imageFilePath);
            var page = Engine.Process(img);
            return page;
        }
    }
}
