namespace Asumet.Doc.Tests
{
    public class BaseTest
    {
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
