namespace Asumet.Doc
{
    using System;
    using System.IO;
    using System.Linq;
    using Asumet.Doc.Match;
    using Asumet.Doc.Ocr;
    using Asumet.Doc.Office;
    using Asumet.Models;
    using Microsoft.Extensions.Configuration;

    /// <summary> Entry point </summary>
    public class Program
    {
        private static string GetOcrFilePath(string imageFilePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(imageFilePath);
            var textFilePath = Path.Combine(AppSettings.Instance.DocumentOutputDirectory, fileName, ".txt");
            return Path.Combine(AppSettings.Instance.DocumentOutputDirectory, textFilePath);
        }

        /// <summary> Entry point </summary>
        public static void Main()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettings.Instance.UpdateConfiguration(configuration);

            Console.WriteLine($"AppSettings Templates Directory: {AppSettings.Instance.TemplatesDirectory}");

            // ExportPsa();
            DoOcr();
            // GetMatchFile();
            Match();
        }

        private static void ExportPsa()
        {
            var psa = Psa.GetPsaStub();
            IOfficeExporter<Psa> psaExporter = new PsaExporter(psa);
            psaExporter.Export();
            Console.WriteLine(psaExporter.OutputFilePath);
        }

        private static void DoOcr()
        {
            var imageFilePath = "./images/PSA-01-300dpi-left.jpg";
            var lines = OcrWrapper.ImageToStrings(imageFilePath);
            var outputFilePath = GetOcrFilePath(imageFilePath);
            File.WriteAllLines(outputFilePath, lines);
        }

        private static void GetMatchFile()
        {
            var psa = Psa.GetPsaStub();
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var lines = matchPattern.GetFilledPattern();
            var s = string.Join(Environment.NewLine, lines.ToArray());
            Console.WriteLine(s);
        }

        private static void Match()
        {
            var psa = Psa.GetPsaStub();
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var matcher = new PsaMatcher(matchPattern);
            var imageFilePath = "./images/PSA-01-300dpi-left.jpg";
            var outputFilePath = GetOcrFilePath(imageFilePath);
            var lines = File.ReadAllLines(outputFilePath);
            var score = matcher.MatchDocumentWithPattern(lines);
            Console.WriteLine($"Match Score = {score}%");
        }
    }
}
