using System.ComponentModel.DataAnnotations;

namespace Asumet.Doc.Dtos
{
    /// <summary> Buyer DTO</summary>
    public class BuyerDto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        public string FullName { get; set; }

        [MaxLength(128)]
        public string Address { get; set; }

        [MaxLength(12)]
        public string Inn { get; set; }

        [StringLength(9)]
        public string Kpp { get; set; }

        [MaxLength(128)]
        public string Bank { get; set; }

        [StringLength(9)]
        public string Bic { get; set; }

        [StringLength(20)]
        public string CorrespondentAccount { get; set; }

        [StringLength(20)]
        public string Account { get; set; }

        [MaxLength(128)]
        public string ResponsiblePerson { get; set; }
    }
}