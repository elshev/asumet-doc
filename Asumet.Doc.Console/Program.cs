namespace Asumet.Doc
{
    using System;
    using System.IO;
    using System.Linq;
    using Asumet.Doc.Match;
    using Asumet.Doc.Ocr;
    using Asumet.Doc.Office;
    using Asumet.Entities;
    using Microsoft.Extensions.Configuration;

    /// <summary> Entry point </summary>
    public class Program
    {
        /// <summary> Entry point </summary>
        public static void Main()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            AppSettings.Instance.UpdateConfiguration(configuration);

            foreach (var c in configuration.AsEnumerable())
            {
                Console.WriteLine(c.Key + " = " + c.Value);
            }

            Console.WriteLine($"AppSettings Templates Directory: {AppSettings.Instance.TemplatesDirectory}");

            // ExportPsa();
            // DoOcr("PSA-01-300dpi-left.jpg");
            // TestOsd("PSA-01-300dpi-left.jpg");
            // GetMatchFile();
            // Match();
        }

        private static string GetOcrFilePath(string imageFilePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(imageFilePath);
            var textFileName = Path.ChangeExtension(fileName, ".txt");
            var textFilePath = Path.Combine(AppSettings.Instance.DocumentOutputDirectory, textFileName);
            return Path.Combine(AppSettings.Instance.DocumentOutputDirectory, textFilePath);
        }

        private static void ExportPsa()
        {
            var psa = PsaSeedData.GetPsa(2);
            IOfficeExporter<Psa> psaExporter = new PsaExporter(psa)
            {
                LeaveMissingPlaceholders = false
            };
            psaExporter.Export();
            Console.WriteLine(psaExporter.OutputFilePath);
        }

        private static string DoOcr(string imageFileName)
        {
            var imageFilePath = Path.Combine("./images", imageFileName);
            var lines = OcrWrapper.ImageToStrings(imageFilePath);
            var outputFilePath = GetOcrFilePath(imageFilePath);
            File.WriteAllLines(outputFilePath, lines);
            return outputFilePath;
        }

/*        private static void TestOsd(string imageFileName)
        {
            var imageFilePath = Path.Combine("./images", imageFileName);
            OcrWrapper.ImageToOsd(imageFilePath);
        }
*/
        private static void GetMatchFile()
        {
            var psa = PsaSeedData.GetPsa(1);
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var lines = matchPattern.GetFilledPattern();
            var s = string.Join(Environment.NewLine, lines.ToArray());
            Console.WriteLine(s);
        }

        private static void Match()
        {
            var outputFilePath = DoOcr("PSA-01-300dpi-left.jpg");
            var lines = File.ReadAllLines(outputFilePath);

            var psa = PsaSeedData.GetPsa(1);
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var matcher = new PsaMatcher(matchPattern);
            var score = matcher.MatchDocumentWithPattern(lines);
            Console.WriteLine($"Match Score = {score}%");
        }
    }
}
