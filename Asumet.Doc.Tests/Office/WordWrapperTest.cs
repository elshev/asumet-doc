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
            const string tts = WordWrapper.TextTableSeparator;
            text.Should().Contain($"Вид лома{tts}Код по ОКПО{tts}Вес брутто, тн{tts}Вес тары, тн{tts}Неметаллические примеси, т.{tts}");
            text.Should().Contain($"Лом и отходы чёрных металлов, 4HH{tts}1111122222{tts}");
            text.Should().Contain($"Лом цветных металлов{tts}333444555{tts}");
            text.Should().Contain($"Итого:{tts}{tts}");
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
            const string tts = WordWrapper.TextTableSeparator;
            text.Should().NotContain($"Вид лома{tts}Код по ОКПО{tts}Вес брутто, тн{tts}Вес тары, тн{tts}Неметаллические примеси, т.{tts}");
            text.Should().Contain($"Лом и отходы чёрных металлов, 4HH{tts}1111122222{tts}");
            text.Should().Contain($"Лом цветных металлов{tts}333444555{tts}");
            text.Should().NotContain($"Итого:{tts}{tts}");
        }
    }
}
