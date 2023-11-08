using Asumet.Doc.Office;

namespace Asumet.Doc.Tests.Office
{
    public class WordWrapperTest : BaseTest
    {
        [Theory]
        [InlineData("PSA-01.docx")]
        public void TestWordFileToTextFile(string wordFileName)
        {
            // Arrange
            var wordFilePath = GetWordFilePath(wordFileName);
            var outputTextFilePath = GetOutputFilePath(wordFileName + ".txt");
            var options = new WordFileToTextOptions();

            // Act
            WordWrapper.WordFileToTextFile(wordFilePath, outputTextFilePath, options);

            // Assert
            var lines = File.ReadAllLines(outputTextFilePath);
            lines.Should().NotBeNullOrEmpty();
            lines.Should().HaveCountGreaterThan(32);
            lines[0].Should().Be("Приложение №1");

            var text = string.Join(Environment.NewLine, lines);
            text.Should().Contain("Получатель лома и отходов: Петр Байер");
            const string ts = WordWrapper.TextTableSeparator;
            text.Should().Contain($"Вид лома{ts}Код по ОКПО{ts}Вес брутто, тн{ts}Вес тары, тн{ts}Неметаллические примеси, т.{ts}");
            text.Should().Contain($"Лом и отходы чёрных металлов, 4HH{ts}1111122222{ts}");
            text.Should().Contain($"Лом цветных металлов{ts}333444555{ts}");
            text.Should().Contain($"Итого:{ts}{ts}");
        }

        [Theory]
        [InlineData("PSA-01.docx")]
        public void TestWordFileToTextFile_WithSkippingTableHeaderAndFooter(string wordFileName)
        {
            // Arrange
            var wordFilePath = GetWordFilePath(wordFileName);
            var outputTextFilePath = GetOutputFilePath(wordFileName + ".txt");
            var options = new WordFileToTextOptions { SkipFirstTableRowCount = 1, SkipLastTableRowCount = 1};

            // Act
            WordWrapper.WordFileToTextFile(wordFilePath, outputTextFilePath, options);

            // Assert
            var lines = File.ReadAllLines(outputTextFilePath);
            lines.Should().NotBeNullOrEmpty();
            lines.Should().HaveCountGreaterThan(32);
            lines[0].Should().Be("Приложение №1");

            var text = string.Join(Environment.NewLine, lines);
            text.Should().Contain("Получатель лома и отходов: Петр Байер");
            const string ts = WordWrapper.TextTableSeparator;
            text.Should().NotContain($"Вид лома{ts}Код по ОКПО{ts}Вес брутто, тн{ts}Вес тары, тн{ts}Неметаллические примеси, т.{ts}");
            text.Should().Contain($"Лом и отходы чёрных металлов, 4HH{ts}1111122222{ts}");
            text.Should().Contain($"Лом цветных металлов{ts}333444555{ts}");
            text.Should().NotContain($"Итого:{ts}{ts}");
        }
    }
}
