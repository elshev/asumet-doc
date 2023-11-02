using Asumet.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asumet.Entities
{
    public class Psa : EntityBase<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get { return base.Id; } set { base.Id = value; } }

        [Required]
        [MaxLength(128)]
        public string ActNumber { get; set; }

        [Required]
        public DateTime? ActDate { get; set; }

        [Required]
        public Buyer Buyer { get; set; }

        [Required]
        public Supplier Supplier { get; set; }

        public List<PsaScrap> PsaScraps { get; set; }

        [MaxLength(64)]
        public string ShortScrapDescription { get; set; }

        [MaxLength(128)]
        public string OwnershipReason { get; set; }

        [MaxLength(128)]
        public string Transport { get; set; }

        public decimal TotalNetto { get; set; }
        
        public string TotalNettoInWords { get; set; }

        public decimal Total { get; set; }

        public string TotalInWords { get; set; }

        public decimal TotalWoNds { get; set; }
        
        public decimal TotalNds { get; set; }

        public string TotalNdsInWords { get; set; }
    }

    /// <summary> Psa seed data </summary>
    public static class PsaSeedData
    {
        public static Psa GetEmptyPsaStub()
        {
            return GetPsa(0);
        }

        public static Psa GetPsa(int id)
        {
            var result = Psas[id];
            if (result.Id == 0)
            {
                result.Id = id;
            }

            return result;
        }

        public static IEnumerable<Psa> GetSeedData()
        {
            return Psas.Skip(1);
        }

        private static List<Psa> Psas
        { 
            get
            {
                return new List<Psa> {
                    new Psa
                    {
                        ActDate = new DateTime(2000, 01, 01).SetKindUtc(),
                        ActNumber = "",
                        Buyer = null,
                        Supplier = null,
                        PsaScraps = null
                    },

                    new Psa
                    {
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
                        Buyer = new Buyer 
                        {
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
                        Supplier = new Supplier
                        {
                            FullName = "Иван Суплаеров",
                            Address = "гор. Орджоникидзе, ул. Московская, д.23/45, кв. 17",
                            Passport = "0055 778899, выд. Ленинским районом, гор. Орджоникидзе",
                        },
                        PsaScraps = new List<PsaScrap>
                        {
                            new PsaScrap
                            {
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
                            new PsaScrap
                            {
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
                    

                    new Psa
                    {
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
                        Buyer = new Buyer 
                        {
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
                        Supplier = new Supplier
                        {
                            FullName = "Сергей Сергеевич Сергеев",
                            Address = "гор. Баку, ул. Бакинская, д.176, кв. 7",
                            Passport = "3444 098774, выд. Бакинским районом, гор. Баку"
                        },
                        PsaScraps = new List<PsaScrap>
                        {
                            new PsaScrap
                            {
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
