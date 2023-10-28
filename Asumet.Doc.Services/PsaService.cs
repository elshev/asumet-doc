using Asumet.Doc.Dtos;
using Asumet.Doc.Repo;

namespace Asumet.Doc.Services
{

    public interface IPsaService : IDocServiceBase
    {
        Task<PsaDto?> GetByIdAsync(int id);
    }

    public class PsaService : DocServiceBase, IPsaService
    {
        public PsaService(IPsaRepository psaRepository)
        {
            PsaRepository = psaRepository;
        }

        protected IPsaRepository PsaRepository { get; }

        public async Task<PsaDto?> GetByIdAsync(int id)
        {
            var psa = await PsaRepository.GetByIdAsync(id);
            var psaDto = new PsaDto();
            psaDto.Id = psa.Id;
            psaDto.ActNumber = psa.ActNumber;
            return psaDto;
        }
    }
}