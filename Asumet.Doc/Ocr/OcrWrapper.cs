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
            IsCommandLineMode = false;
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
            process.StartInfo.FileName = "tesseract";
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
                throw new Exception($"Tesseract process timed out ({ocrTimeout / 1000}seconds). Output:{Environment.NewLine}{sb}");
            }

            return outputFilePath + ".txt";
        }

        /*        /// <summary>
                /// Do OSD
                /// </summary>
                /// <param name="imageFilePath">Path to an image file</param>
                public static void ImageToOsd(string imageFilePath)
                {
                    using var pix = Pix.LoadFromFile(imageFilePath);
                    using var page = Engine.Process(pix, PageSegMode.OsdOnly);
                    page.DetectBestOrientation(out int orientation, out float confidence);
                    Console.WriteLine($"Orientation = {orientation}, Confidence = {confidence}");
                }
        */

        /// <summary>
        /// Parses an image from <paramref name="imageFilePath"/> and returns <see cref="Tesseract.Page"/>
        /// </summary>
        /// <param name="imageFilePath">Path to an image file</param>
        /// <returns>A parsed page.</returns>
        /// <remarks>Don't forget to use 'using' with the result of this method</remarks>
        /// <example>using var page = ImageToPage('path/to/image.png')</example>
        private static Page ImageToPage(string imageFilePath)
        {
            ArgumentNullException.ThrowIfNull(nameof(imageFilePath));
            if (Engine == null)
            {
                throw new NullReferenceException(nameof(Engine));
            }

            using var initialPix = Pix.LoadFromFile(imageFilePath);
            using var pix = GetNormalizedImage(initialPix);
            var page = Engine.Process(pix);

            return page;
        }

        private static double DegreesToRadians(double degrees)
        {
            double radians = Math.PI / 180 * degrees;
            return radians;
        }

        /// <summary>
        /// Determines if the image in the <paramref name="pix"/> is rotated by 90, 180, 270 degrees.
        /// If so, rotate it to Up orientation.
        /// </summary>
        /// <param name="pix">An image</param>
        /// <returns>A new <see cref="Pix"/> object with image in Up orientation</returns>
        private static Pix GetNormalizedImage(Pix pix)
        {
            ArgumentNullException.ThrowIfNull(nameof(pix));
            if (Engine == null)
            {
                throw new NullReferenceException(nameof(Engine));
            }

            using var page = Engine.Process(pix, PageSegMode.AutoOsd);
            using var pageIter = page.AnalyseLayout();
            pageIter.Begin();
            var pageProps = pageIter.GetProperties();
            var orientation = pageProps.Orientation;
            var result = pix;
            switch (orientation)
            {
                case Orientation.PageRight:
                    result = pix.Rotate90(-1);
                    break;
                case Orientation.PageLeft:
                    result = pix.Rotate90(1);
                    break;
                case Orientation.PageDown:
                    var radians = (float)DegreesToRadians(180);
                    result = pix.Rotate(radians);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
