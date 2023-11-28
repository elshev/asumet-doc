namespace Asumet.Doc.Ocr
{
    using Asumet.Common;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Tesseract;

    /// <inheritdoc/>
    public class OcrWrapper : IOcrWrapper
    {
        /// <summary>Static constructor</summary>
        public OcrWrapper(IAppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        /// <summary>
        /// A workaround for Tesseract NuGet package that doesn't work in MS .net6 Bullseye Docker image.
        /// Set this property to True if you want to run Tesseract via command line.
        /// </summary>
        /// <remarks>
        /// This workaround is not necessary anymore.
        /// Tesseract works fine in the MS .net6 Bookworm Docker image.
        /// But was left here as an option.
        /// </remarks>
        /// <example>IsCommandLineMode = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);</example>
        private bool IsCommandLineMode { get; } = false;

        private static TesseractEngine? _engine = null;

        private readonly object initializeEngineLock = new();

        private TesseractEngine Engine
        {
            get
            {
                if (_engine != null)
                {
                    return _engine;
                }

                lock (initializeEngineLock)
                {
                    _engine ??= new TesseractEngine(AppSettings.TesseractDataDirectory, "rus", EngineMode.Default);
                }

                return _engine;
            }
        }

        /// <summary>Application Settings</summary>
        public IAppSettings AppSettings { get; }

        /// <inheritdoc/>
        public IEnumerable<string> ImageToStrings(string imageFilePath)
        {
            ArgumentNullException.ThrowIfNull(imageFilePath, nameof(imageFilePath));

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

        /// <summary>
        /// Parses an image from <paramref name="imageFilePath"/> and returns <see cref="Tesseract.Page"/>
        /// </summary>
        /// <param name="imageFilePath">Path to an image file</param>
        /// <returns>A parsed page.</returns>
        /// <remarks>Don't forget to use 'using' with the result of this method</remarks>
        /// <example>using var page = ImageToPage('path/to/image.png')</example>
        private Page ImageToPage(string imageFilePath)
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
        private Pix GetNormalizedImage(Pix pix)
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
