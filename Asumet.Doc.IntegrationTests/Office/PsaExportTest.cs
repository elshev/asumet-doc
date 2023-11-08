namespace Asumet.Doc.IntegrationTests.Office
{
    using Asumet.Doc.Office;
    using Asumet.Entities;
    using NPOI.XWPF.UserModel;

    public class PsaExportTest : IntegrationTestBase
    {
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
            var psa = GetPsa();
            IOfficeExporter<Psa> psaExporter = new PsaExporter();

            // Act
            var filePath = psaExporter.Export(psa);

            // Assert
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
