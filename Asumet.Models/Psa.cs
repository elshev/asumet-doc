﻿using System.ComponentModel.DataAnnotations;

namespace Asumet.Models
{
    public class Psa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ActNumber { get; set; }

        [Required]
        public DateTime? ActDate { get; set; }

        [Required]
        public Buyer Buyer { get; set; }

        [Required]
        public Supplier Supplier { get; set; }

        public List<PsaScrap> PsaScraps { get; set; }

        public string ShortScrapDescription { get; set; }

        public string OwnershipReason { get; set; }

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
            return PsaStubs[0];
        }

        public static Psa GetPsa(int id)
        {
            return PsaStubs[id];
        }

        private static IDictionary<int, Psa> PsaStubs
        { 
            get
            {
                return new Dictionary<int, Psa> {
                    { 
                        0,
                        new Psa
                        {
                            Id = 0,
                            ActDate = new DateTime(2000, 01, 01),
                            ActNumber = "",
                            Buyer = null,
                            Supplier = null,
                            PsaScraps = null
                        }
                    },

                    { 
                        1,
                        new Psa
                        {
                            ActDate = new DateTime(2023, 10, 18, 15, 52, 09),
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
                                Inn = "11112222333",
                                Kpp = "99988877",
                                Bank = "ЗАО \"TecтБанк\"",
                                Bic = "083083083",
                                CorrespondentAccount = "30000000000000000000",
                                Account = "4080000000000001111",
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
                        } 
                    },

                    { 
                        2,
                        new Psa
                        {
                            ActDate = new DateTime(2023, 09, 08, 05, 16, 55),
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
                                Kpp = "08345745443",
                                Bank = "ПАО Сбербанк",
                                Bic = "0278502780",
                                CorrespondentAccount = "3000000005924900554",
                                Account = "4080009028804994890222",
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
                    }
                };
            }
        }
    }
}
