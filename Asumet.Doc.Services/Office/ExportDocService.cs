using Asumet.Doc.Office;
using Asumet.Doc.Repo;
using Asumet.Entities;
using AutoMapper;

namespace Asumet.Doc.Services.Office
{
    public class ExportDocService : DocServiceBase, IExportDocService
    {
        public ExportDocService(
            IMapper mapper,
            IPsaRepository psaRepository,
            IOfficeExporter<Psa> officeExporter
            )
        {
            PsaRepository = psaRepository;
            OfficeExporter = officeExporter;
            Mapper = mapper;
        }

        protected IPsaRepository PsaRepository { get; }
        public IOfficeExporter<Psa> OfficeExporter { get; }
        public IMapper Mapper { get; }

        /// <inheritdoc/>
        public async Task<string?> ExportPsaToWordFileAsync(int id)
        {
            var psa = await PsaRepository.GetByIdAsync(id);
            if (psa == null)
            {
                return null;
            }

            var result = ExportPsaToWord(psa);
            return result;
        }

        private string ExportPsaToWord(Psa psa)
        {
            OfficeExporter.Export(psa);
            return OfficeExporter.OutputFilePath;
        }
    }
}