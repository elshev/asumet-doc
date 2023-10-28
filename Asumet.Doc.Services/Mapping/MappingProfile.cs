namespace Asumet.Doc.Services.Mapping
{
    using Asumet.Doc.Dtos;
    using Asumet.Entities;
    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Buyer, BuyerDto>();
            CreateMap<BuyerDto, Buyer>();

            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>();

            CreateMap<PsaScrap, PsaScrapDto>();
            CreateMap<PsaScrapDto, PsaScrap>();

            CreateMap<Psa, PsaDto>();
            CreateMap<PsaDto, Psa>();
        }

        public override string ProfileName => GetType().Name;
    }
}