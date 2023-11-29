using Asumet.Doc.Dtos;
using Asumet.Doc.Repo;
using Asumet.Entities;
using AutoMapper;

namespace Asumet.Doc.Services.Data
{
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
            var psaDto = Mapper.Map<PsaDto>(psa);
            return psaDto;
        }

        public async Task<PsaDto?> InsertEntityAsync(PsaDto psaDto)
        {
            if (Mapper.Map<Psa>(psaDto) is not Psa psaToInsert)
            {
                return null;
            }

            var psa = await PsaRepository.InsertEntityAsync(psaToInsert);
            var result = Mapper.Map<PsaDto>(psa);
            return result;
        }
    }
}
