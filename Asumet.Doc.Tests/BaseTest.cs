using Moq;

namespace Asumet.Doc.Tests
{
    public class BaseTest
    {
        protected static Mock<IAppSettings> CreateAppSettings()
        {
            var appSettings = new Mock<IAppSettings>();
            appSettings
                .Setup(s => s.TesseractDataDirectory)
                .Returns("./tessdata");

            return appSettings;
        }

        protected static string GetWordFilePath(string fileName)
        {
            var result = Path.Combine("./TestInput/Office", fileName);
            return result;            
        }

        protected static string GetOutputFilePath(string fileName)
        {
            var result = Path.Combine("./TestOutput", fileName);
            return result;            
        }
    }
}
