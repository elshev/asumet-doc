namespace Asumet.Doc
{
    using System;
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

            Console.WriteLine($"Templates Directory: {AppSettings.Instance.TemplatesDirectory}");

            var psa = Psa.GetPsaStub();
            var psaExporter = new PsaExporter(psa);
            psaExporter.Export();
            Console.WriteLine(psaExporter.OutputFilePath);
        }
    }
}
