namespace Asumet.Doc.Services.Office
{
    public interface IExportDocService : IDocServiceBase
    {
        Task ExportPsaToWordAsync(int id, Stream stream);
    }
}