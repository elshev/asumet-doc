namespace Asumet.Models
{
    public class ScrapItem
    {
        public string? Name { get; set; }
        
        public string? Okpo { get; set; }

        public decimal GrossWeight { get; set; }

        public decimal TareWeight { get; set; }

        public decimal NonmetallicMixtures { get; set; }

        public decimal MixturePercentage { get; set; }

        public decimal NetWeight { get; set; }

        public decimal Price { get; set; }

        public decimal SumWoNds { get; set; }

        public decimal Sum { get; set; }
    }
}
