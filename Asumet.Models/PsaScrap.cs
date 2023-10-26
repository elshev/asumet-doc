using System.ComponentModel.DataAnnotations;

namespace Asumet.Models
{
    /// <summary> PsaScrap </summary>
    public class PsaScrap
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Okpo { get; set; }

        [Required]
        public Psa Psa { get; set; }

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
