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
            // DoOcr();
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

        private static string OcrFilePath
        {
            get { return Path.Combine(AppSettings.Instance.DocumentOutputDirectory, "PSA-01-144dpi.txt"); }
        }

        private static void DoOcr()
        {
            var lines = OcrWrapper.ImageToStrings("./images/PSA-01-144dpi.png");
            var outputFilePath = OcrFilePath;
            File.WriteAllLines(outputFilePath, lines);
        }

        private static void GetMatchFile()
        {
            var psa = Psa.GetPsaStub();
            IWordMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var lines = matchPattern.GetFilledPattern();
            var s = string.Join(Environment.NewLine, lines.ToArray());
            Console.WriteLine(s);
        }

        private static void Match()
        {
            var psa = Psa.GetPsaStub();
            IWordMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var matcher = new PsaMatcher(matchPattern);
            var lines = File.ReadAllLines(OcrFilePath);
            var score = matcher.MatchDocumentWithPattern(lines);
            Console.WriteLine(score);
        }
    }
}
