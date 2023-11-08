namespace Asumet.Doc.Services.Office
{
    public interface IExportDocService : IDocServiceBase
    {
        Task<string?> ExportPsaToWordFileAsync(int id);
    }
}