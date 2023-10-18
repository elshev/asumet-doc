﻿namespace Asumet.Models
{
    /// <summary> Psa stub </summary>
    public class Psa
    {
        public string? ActNumber { get; set; }
        public DateTime? ActDate { get; set; }
        
        /// <summary> Buyer </summary>
        public Buyer? Buyer { get; set; }

        /// <summary> Supplier </summary>
        public Supplier? Supplier { get; set; }

        public Scrap? Scrap { get; set; }

        
        public static Psa GetPsaStub()
        {
            var buyer = new Buyer 
            {
                FullName = "Петр Байер",
                Address = "гор. Москва, Индустриальная ул., д.123, оф. 987",
                Inn = "11112222333",
                Kpp = "99988877",
                Bank = "ЗАО \"TectБанк\"",
                Bic = "083083083",
                CorrespondentAccount = "30000000000000000000",
                Account = "4080000000000001111",
                ResponsiblePerson = "Воронцов Сергей Михайлович"
            };
            var supplier = new Supplier
            {
                FullName = "Иван Суплаеров",
                Address = "гор. Орджоникидзе, ул. Московская, д.23/45, кв. 17",
                Passport = "0055 778899, выд. Ленинским районом, гор. Орджоникидзе",
                OwnershipReason = "Личное имущество",
                Transport = "Автомобиль"
            };
            
            var scrap = new Scrap
            {
                ShortDescription = "Разный металлолом",
                TotalNetto = 4.464m,
                TotalNettoInWords = "Четыре тонны 466 кг",
                Total = 120974.40m,
                TotalInWords = "Сто двадцать тысяч девятсот семьдесят четыре рубля 40 копеек",
                TotalNds = 0,
                TotalNdsInWords = "Ноль рублей 00 коп.",
                TotalWoNds = 120974.40m,
                ScrapItems = new List<ScrapItem>
                {
                    new ScrapItem
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
                    new ScrapItem
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
            };

            var psa = new Psa
            {
                ActDate = DateTime.Now,
                ActNumber = "А-012345",
                Buyer = buyer,
                Supplier = supplier,
                Scrap = scrap
            };

            return psa;
        }

    }
}
