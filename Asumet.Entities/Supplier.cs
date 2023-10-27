using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asumet.Entities
{
    /// <summary> Supplier </summary>
    public class Supplier : EntityBase<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get { return base.Id; } set { base.Id = value; } }

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
