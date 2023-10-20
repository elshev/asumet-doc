namespace Asumet.Doc
{
    using System;
    using System.Linq;
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
            GetMatchFile();
        }

        private static void ExportPsa()
        {
            var psa = Psa.GetPsaStub();
            var psaExporter = new PsaExporter(psa);
            psaExporter.Export();
            Console.WriteLine(psaExporter.OutputFilePath);
        }

        private static void DoOcr()
        {
            var lines = OcrWrapper.ImageToStrings("./images/PSA-01-144dpi.png");
            lines.ToList().ForEach(s => Console.WriteLine(s));
        }

        private static void GetMatchFile()
        {
            var psa = Psa.GetPsaStub();
            var matchPattern = new PsaMatchPattern(psa);
            var lines = matchPattern.GetFilledPattern();
            var s = string.Join(Environment.NewLine, lines.ToArray());
            Console.WriteLine(s);
        }
    }
}
