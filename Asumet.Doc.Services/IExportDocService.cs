using Asumet.Doc.Dtos;

namespace Asumet.Doc.Services
{
    public interface IExportDocService : IDocServiceBase
    {
        Task<string?> ExportPsaToWordFileAsync(int id);

        Task<string?> ExportPsaToWordFileAsync(PsaDto psaDto);

    }
}