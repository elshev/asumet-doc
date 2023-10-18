namespace Asumet.Models
{
    /// <summary> Scrap stub </summary>
    public class Scrap
    {
        public string? ShortDescription { get; set; }

        public decimal TotalNetto { get; set; }
        
        public string? TotalNettoInWords { get; set; }

        public decimal Total { get; set; }

        public string? TotalInWords { get; set; }

        public decimal TotalWoNds { get; set; }
        
        public decimal TotalNds { get; set; }

        public string? TotalNdsInWords { get; set; }


        public List<ScrapItem> ScrapItems { get; set; } = new List<ScrapItem>();
    }
}
