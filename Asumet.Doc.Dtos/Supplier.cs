using System.ComponentModel.DataAnnotations;

namespace Asumet.Doc.Dtos
{
    /// <summary> Supplier DTO</summary>
    public class SupplierDto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        /// <summary> Full name </summary>
        public string FullName { get; set; }

        [MaxLength(128)]
        public string Address { get; set; }

        [MaxLength(128)]
        public string Passport { get; set; }
    }
}
