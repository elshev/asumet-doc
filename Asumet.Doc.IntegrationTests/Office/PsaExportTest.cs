﻿using Asumet.Doc.Office;
using Asumet.Models;
using Microsoft.Extensions.Configuration;
using NPOI.XWPF.UserModel;

namespace Asumet.Doc.IntegrationTests.Office
{
    public class PsaExportTest
    {
        public PsaExportTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettings.Instance.UpdateConfiguration(configuration);
        }

        private static void AssertDocxFile(string filePath)
        {
            filePath.Should().NotBeNull();
            File.Exists(filePath).Should().BeTrue();
            var fileName = Path.GetFileName(filePath);
            fileName.StartsWith("ПСА").Should().BeTrue();
            var fileExtension = Path.GetExtension(filePath);
            fileExtension.Should().Be(".docx");
        }

        private static bool AreTextsExistInDoc(XWPFDocument doc, IEnumerable<string> stringsToFind)
        {
            var found = stringsToFind.ToDictionary(s => s, s => false);
            foreach (var paragraph in doc.Paragraphs)
            {
                foreach (var s in found.Keys)
                {
                    found[s] = found[s] || paragraph.ParagraphText.Contains(s);
                }
                if (found.All(kvp => kvp.Value))
                {
                    return true;
                }
            }

            return false;
        }

        [Fact]
        public void TestExportPsa()
        {
            // Arrange
            var psa = Psa.GetPsaStub();
            IOfficeExporter<Psa> psaExporter = new PsaExporter(psa);

            // Act
            psaExporter.Export();

            // Assert
            var filePath = psaExporter.OutputFilePath;
            AssertDocxFile(filePath);

            using var rs = File.OpenRead(filePath);
            using var doc = new XWPFDocument(rs);

            var table = doc.Tables.Where(t => t.NumberOfRows > 0).FirstOrDefault();
            Assert.NotNull(table);
            table.Rows.Count.Should().BeGreaterThan(3);
            AreTextsExistInDoc(doc, new string[] { psa.ActNumber ?? "", psa.Buyer?.FullName ?? "", psa.Supplier?.FullName ?? "" })
                .Should().BeTrue();
        }
    }
}
