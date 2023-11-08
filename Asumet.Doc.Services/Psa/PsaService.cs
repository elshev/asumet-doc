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
            var psaDto = Mapper.Map(psa, typeof(Psa), typeof(PsaDto)) as PsaDto;
            return psaDto;
        }

        public async Task<PsaDto?> InsertEntityAsync(PsaDto psaDto)
        {
            if (Mapper.Map(psaDto, typeof(PsaDto), typeof(Psa)) is not Psa psaToInsert)
            {
                return null;
            }

            var psa = await PsaRepository.InsertEntityAsync(psaToInsert);
            var result = Mapper.Map(psa, typeof(Psa), typeof(PsaDto)) as PsaDto;
            return result;
        }
    }
}
