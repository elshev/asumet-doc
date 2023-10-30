using Asumet.Doc.Dtos;
using Asumet.Doc.Office;
using Asumet.Doc.Repo;
using Asumet.Entities;
using AutoMapper;

namespace Asumet.Doc.Services
{
    public class ExportDocService : DocServiceBase, IExportDocService
    {
        public ExportDocService(
            IPsaRepository psaRepository,
            IBuyerRepository buyerRepository,
            ISupplierRepository supplierRepository,
            IMapper mapper)
        {
            PsaRepository = psaRepository;
            BuyerRepository = buyerRepository;
            SupplierRepository = supplierRepository;
            Mapper = mapper;
        }

        protected IPsaRepository PsaRepository { get; }
        public IBuyerRepository BuyerRepository { get; }
        public ISupplierRepository SupplierRepository { get; }
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

        /// <inheritdoc/>
        public async Task<string?> ExportPsaToWordFileAsync(PsaDto psaDto)
        {
            if (Mapper.Map(psaDto, typeof(PsaDto), typeof(Psa)) is not Psa psa)
            {
                return null;
            }

            if (psa.Buyer.Id > 0)
            {
                psa.Buyer = await BuyerRepository.GetByIdAsync(psa.Buyer.Id);
            }
            
            if (psa.Supplier.Id > 0)
            {
                psa.Supplier = await SupplierRepository.GetByIdAsync(psa.Supplier.Id);
            }
            
            await PsaRepository.InsertEntityAsync(psa);

            var result = ExportPsaToWord(psa);
            return result;
        }

        private static string ExportPsaToWord(Psa psa)
        {
            var psaExporter = new PsaExporter(psa);
            psaExporter.Export();

            return psaExporter.OutputFilePath;
        }
    }
}