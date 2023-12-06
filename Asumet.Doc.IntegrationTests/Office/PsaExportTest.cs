namespace Asumet.Doc.IntegrationTests.Office
{
    using Asumet.Doc.Api;
    using Asumet.Doc.Office;
    using Asumet.Entities;
    using NPOI.XWPF.UserModel;

    public class PsaExportTest : ApiIntegrationTestBase
    {
        public PsaExportTest(ApiTestWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        protected static void AssertDocxFile(string filePath)
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
            var psa = GetPsa();
            var psaExporter = GetService<IOfficeExporter<Psa>>();
            using var docStream = new MemoryStream();

            // Act
            psaExporter.Export(psa, docStream);

            // Assert
            //AssertDocxFile(filePath);

            //using var rs = File.OpenRead(filePath);
            docStream.Seek(0, SeekOrigin.Begin);
            using var doc = new XWPFDocument(docStream);

            var table = doc.Tables.Where(t => t.NumberOfRows > 0).FirstOrDefault();
            Assert.NotNull(table);
            table.Rows.Count.Should().BeGreaterThan(3);
            AreTextsExistInDoc(doc, new string[] { psa.ActNumber ?? "", psa.Buyer?.FullName ?? "", psa.Supplier?.FullName ?? "" })
                .Should().BeTrue();
        }
    }
}
