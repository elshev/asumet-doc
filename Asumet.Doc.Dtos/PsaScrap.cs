using System.ComponentModel.DataAnnotations;

namespace Asumet.Doc.Dtos
{
    /// <summary> PsaScrap Dto </summary>
    public class PsaScrapDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        
        [MaxLength(14)]
        [Required]
        public string Okpo { get; set; }

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
