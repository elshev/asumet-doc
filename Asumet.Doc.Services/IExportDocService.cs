namespace Asumet.Doc.Services
{
    public interface IExportDocService : IDocServiceBase
    {
        Task<string?> ExportPsaToWordFileAsync(int id);
    }
}