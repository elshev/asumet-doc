using Asumet.Doc.Dtos;
using Asumet.Doc.Repo;
using Asumet.Entities;
using AutoMapper;

namespace Asumet.Doc.Services
{

    public interface IPsaService : IDocServiceBase
    {
        Task<PsaDto?> GetByIdAsync(int id);
    }

    public class PsaService : DocServiceBase, IPsaService
    {
        public PsaService(IPsaRepository psaRepository, IMapper mapper)
        {
            PsaRepository = psaRepository;
            Mapper = mapper;
        }

        protected IPsaRepository PsaRepository { get; }
        public IMapper Mapper { get; }

        public async Task<PsaDto?> GetByIdAsync(int id)
        {
            var psa = await PsaRepository.GetByIdAsync(id);
            var psaDto = Mapper.Map(psa, typeof(Psa), typeof(PsaDto)) as PsaDto;
            return psaDto;
        }
    }
}