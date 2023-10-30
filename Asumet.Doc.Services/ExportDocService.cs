using Asumet.Doc.Dtos;
using Asumet.Doc.Office;
using Asumet.Doc.Repo;
using Asumet.Entities;
using AutoMapper;

namespace Asumet.Doc.Services
{
    public class ExportDocService : DocServiceBase, IExportDocService
    {
        public ExportDocService(IPsaRepository psaRepository, IMapper mapper)
        {
            PsaRepository = psaRepository;
            Mapper = mapper;
        }

        protected IPsaRepository PsaRepository { get; }
        
        public IMapper Mapper { get; }

        /// <inheritdoc/>
        public async Task<string?> ExportPsaToWordFileAsync(int id)
        {
            var psa = await PsaRepository.GetByIdAsync(id);
            var result = ExportPsaToWord(psa);
            return result;
        }

        /// <inheritdoc/>
        public async Task<string?> ExportPsaToWordFileAsync(PsaDto psaDto)
        {
            if (Mapper.Map(psaDto, typeof(PsaDto), typeof(Psa)) is not Psa psa)
            {
                return null;
            }

            var result = ExportPsaToWord(psa);
            return result;
        }

        private string ExportPsaToWord(Psa psa)
        {
            var psaExporter = new PsaExporter(psa);
            psaExporter.Export();

            return psaExporter.OutputFilePath;
        }
    }
}