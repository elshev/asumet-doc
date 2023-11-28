using Asumet.Doc.Ocr;

namespace Asumet.Doc.Tests.Office
{
    public class OcrWrapperTest : BaseTest
    {
        [Fact]
        public void ImageToStrings_WhenNullPassed_ThrowsArgumentNullException()
        {
            // Arrange
            var appSettings = CreateAppSettings();
            var ocrWrapper = new OcrWrapper(appSettings.Object);

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ocrWrapper.ImageToStrings(null));
        }
    }
}
