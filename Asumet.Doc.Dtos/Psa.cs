using System.ComponentModel.DataAnnotations;

namespace Asumet.Doc.Dtos
{
    /// <summary> Psa DTO</summary>
    public class PsaDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string ActNumber { get; set; }

        [Required]
        public DateTime? ActDate { get; set; }

        [Required]
        public BuyerDto Buyer { get; set; }

        [Required]
        public SupplierDto Supplier { get; set; }

        public List<PsaScrapDto> PsaScraps { get; set; }

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
}
