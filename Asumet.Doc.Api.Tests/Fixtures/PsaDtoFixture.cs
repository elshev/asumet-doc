using Asumet.Doc.Dtos;
using Asumet.Common;

namespace Asumet.Doc.Api.Tests.Fixtures
{
    internal static class PsaDtoFixture
    {
        public static PsaDto GetEmptyPsa()
        {
            return new PsaDto
            {
                ActDate = new DateTime(2000, 01, 01).SetKindUtc(),
                ActNumber = "",
                Buyer = null,
                Supplier = null,
                PsaScraps = null
            };
        }

        public static PsaDto? GetById(int id)
        {
            return id > PsaList.Count ? null : PsaList[id - 1];
        }

        public static List<PsaDto> PsaList
        { 
            get
            {
                return new List<PsaDto>
                {
                    new PsaDto
                    {
                        Id = 1,
                        ActDate = new DateTime(2023, 10, 18, 15, 52, 09).SetKindUtc(),
                        ActNumber = "А-012345",
                        OwnershipReason = "Личное имущество",
                        Transport = "Автомобиль",
                        ShortScrapDescription = "Разный металлолом",
                        TotalNetto = 4.464m,
                        TotalNettoInWords = "Четыре тонны 466 кг",
                        Total = 120974.40m,
                        TotalInWords = "Сто двадцать тысяч девятсот семьдесят четыре рубля 40 копеек",
                        TotalNds = 0,
                        TotalNdsInWords = "Ноль рублей 00 коп.",
                        TotalWoNds = 120974.40m,
                        Buyer = new BuyerDto
                        {
                            Id = 1,
                            FullName = "Петр Байер",
                            Address = "гор. Москва, Индустриальная ул., д.123, оф. 987",
                            Inn = "111122223333",
                            Kpp = "999888777",
                            Bank = "ЗАО \"TecтБанк\"",
                            Bic = "083083083",
                            CorrespondentAccount = "30000000000000000083",
                            Account = "40800000000000011111",
                            ResponsiblePerson = "Воронцов Сергей Михайлович"
                        },
                        Supplier = new SupplierDto
                        {
                            Id = 1,
                            FullName = "Иван Суплаеров",
                            Address = "гор. Орджоникидзе, ул. Московская, д.23/45, кв. 17",
                            Passport = "0055 778899, выд. Ленинским районом, гор. Орджоникидзе",
                        },
                        PsaScraps = new List<PsaScrapDto>
                        {
                            new PsaScrapDto
                            {
                                Id = 1,
                                Name = "Лом и отходы чёрных металлов, 4HH",
                                Okpo = "1111122222",
                                GrossWeight = 19.26m,
                                TareWeight = 14.46m,
                                NonmetallicMixtures = 0.101m,
                                MixturePercentage = 5m,
                                NetWeight = 4.464m,
                                Price = 27100.00m,
                                SumWoNds = 120974.40m,
                                Sum = 120974.40m
                            },
                            new PsaScrapDto
                            {
                                Id = 2,
                                Name = "Лом цветных металлов",
                                Okpo = "333444555",
                                GrossWeight = 3.25m,
                                TareWeight = 1.55m,
                                NonmetallicMixtures = 0.2m,
                                MixturePercentage = 6.50m,
                                NetWeight = 1.7m,
                                Price = 100000.00m,
                                SumWoNds = 170000.00m,
                                Sum = 170000.00m
                            },
                        }
                    },

                    new PsaDto
                    {
                        Id = 2,
                        ActDate = new DateTime(2023, 09, 08, 05, 16, 55).SetKindUtc(),
                        ActNumber = "А-9736115",
                        OwnershipReason = "Личное имущество",
                        Transport = "Автомобиль",
                        ShortScrapDescription = "Разный металлолом",
                        TotalNetto = 2.123m,
                        TotalNettoInWords = "Две тонны 123 кг",
                        Total = 43567.88m,
                        TotalInWords = "Сорок три тысячи пятьсот шестьдесят семь рублей 88 копеек",
                        TotalNds = 0,
                        TotalNdsInWords = "Ноль рублей 00 коп.",
                        TotalWoNds = 43567.88m,
                        Buyer = new BuyerDto
                        {
                            Id = 2,
                            FullName = "ООО 'Лом и отходы'",
                            Address = "гор. Воронеж, Варшавский пр., д.23",
                            Inn = "98345098453",
                            Kpp = "083457454",
                            Bank = "ПАО Сбербанк",
                            Bic = "027850278",
                            CorrespondentAccount = "30000000000000000278",
                            Account = "40800090288049948902",
                            ResponsiblePerson = "Приёмкин Рудольф Викторович"
                        },
                        Supplier = new SupplierDto
                        {
                            Id = 2,
                            FullName = "Сергей Сергеевич Сергеев",
                            Address = "гор. Баку, ул. Бакинская, д.176, кв. 7",
                            Passport = "3444 098774, выд. Бакинским районом, гор. Баку"
                        },
                        PsaScraps = new List<PsaScrapDto>
                        {
                            new PsaScrapDto
                            {
                                Id = 3,
                                Name = "Лом и отходы чёрных металлов, 4HH",
                                Okpo = "1111122222",
                                GrossWeight = 9.55m,
                                TareWeight = 10.6m,
                                NonmetallicMixtures = 0.05m,
                                MixturePercentage = 6m,
                                NetWeight = 2.123m,
                                Price = 22000.00m,
                                SumWoNds = 43567.88m,
                                Sum = 43567.88m
                            }
                        }
                    } 
                };
            }
        }
    }
}
