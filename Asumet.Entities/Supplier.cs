using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asumet.Entities
{
    /// <summary> Supplier stub </summary>
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(128)]
        /// <summary> Full name </summary>
        public string FullName { get; set; }

        [MaxLength(128)]
        public string Address { get; set; }

        [MaxLength(128)]
        public string Passport { get; set; }
    }
}
