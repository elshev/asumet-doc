using Asumet.Doc.Office;

namespace Asumet.Doc.Tests.Office
{
    public class WordWrapperTest : BaseTest
    {
        [Theory]
        [InlineData("PSA-01.docx")]
        public void TestSaveAsText(string wordFileName)
        {
            // Arrange
            var wordFilePath = GetWordFilePath(wordFileName);
            var outputTextFilePath = GetOutputFilePath(wordFileName + ".txt");

            // Act
            WordWrapper.GetWordFileAsTextFile(wordFilePath, outputTextFilePath);

            // Assert
            var lines = File.ReadAllLines(outputTextFilePath);
            lines.Should().NotBeNullOrEmpty();
            lines[0].Should().Be("Приложение №1");
        }

    }
}
