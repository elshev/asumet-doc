using Asumet.Doc.Dtos;

namespace Asumet.Doc.Services.Data
{
    public interface IPsaService : IDocServiceBase
    {
        Task<PsaDto?> GetByIdAsync(int id);

        Task<PsaDto?> InsertEntityAsync(PsaDto psaDto);
    }
}