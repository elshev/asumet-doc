﻿namespace Asumet.Doc
{
    using System;
    using System.IO;
    using Asumet.Doc.Ocr;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The Console application is used only for testing runs.
    /// </summary>
    public class Program
    {
        /// <summary>Static constructor</summary>
        static Program()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            AppSettings = new AppSettings(Configuration);
        }

        private static IConfigurationRoot Configuration { get; }

        private static AppSettings AppSettings { get; }

        /// <summary>Entry point</summary>
        public static void Main()
        {
            Console.WriteLine($"AppSettings Templates Directory: {AppSettings.TemplatesDirectory}");

            // ExportPsa();
            // DoOcr("PSA-01-300dpi-left.jpg");
            // TestOsd("PSA-01-300dpi-left.jpg");
            // GetMatchFile();
            // Match();
            DoOcrForRotatedImages("PSA-01-144dpi.png");
            DoOcrForRotatedImages("PSA-01-144dpi-RotateLeft.png");
        }

        private static string GetImageFilePath(string imageFileName)
        {
            var result = Path.Combine("./images", imageFileName);
            return result;
        }

        private static string GetOcrFilePath(string imageFilePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(imageFilePath);
            var textFileName = Path.ChangeExtension(fileName, ".txt");
            var textFilePath = Path.Combine(AppSettings.DocumentOutputDirectory, textFileName);
            return Path.Combine(AppSettings.DocumentOutputDirectory, textFilePath);
        }

/*
        private static void ExportPsa()
        {
            var psa = PsaSeedData.GetPsa(2);
            IOfficeExporter<Psa> psaExporter = new PsaExporter()
            {
                LeaveMissingPlaceholders = false
            };
            var outputFilePath = psaExporter.Export(psa);
            Console.WriteLine(outputFilePath);
        }
*/
/*
        private static string DoOcr(string imageFileName)
        {
            var imageFilePath = GetImageFilePath(imageFileName);
            var lines = OcrWrapper.ImageToStrings(imageFilePath);
            var outputFilePath = GetOcrFilePath(imageFilePath);
            File.WriteAllLines(outputFilePath, lines);
            return outputFilePath;
        }
*/
/*        private static void TestOsd(string imageFileName)
        {
            var imageFilePath = Path.Combine("./images", imageFileName);
            OcrWrapper.ImageToOsd(imageFilePath);
        }
*/

        private static string DoOcrForRotatedImages(string imageFileName)
        {
            var imageFilePath = GetImageFilePath(imageFileName);
            var ocrWrapper = new OcrWrapper(AppSettings);
            var lines = ocrWrapper.ImageToStrings(imageFilePath);
            var outputFilePath = GetOcrFilePath(imageFilePath);
            File.WriteAllLines(outputFilePath, lines);
            return outputFilePath;
        }

/*
        private static void GetMatchFile()
        {
            var psa = PsaSeedData.GetPsa(1);
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern();
            var lines = matchPattern.GetFilledPattern(psa);
            var s = string.Join(Environment.NewLine, lines.ToArray());
            Console.WriteLine(s);
        }
*/
/*
        private static void Match()
        {
            var imageFilePath = GetImageFilePath("PSA-01-300dpi-left.jpg");
            var psa = PsaSeedData.GetPsa(1);
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern();
            var matcher = new PsaMatcher(matchPattern, new PsaExporter());
            var scoreTask = matcher.MatchDocumentImageWithPatternAsync(imageFilePath, psa);
            scoreTask.Wait();
            var score = scoreTask.Result;
            Console.WriteLine($"Match Score = {score}%");
        }
*/
    }
}
