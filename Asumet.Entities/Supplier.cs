using System.ComponentModel.DataAnnotations;

namespace Asumet.Entities
{
    /// <summary> Supplier </summary>
    public class Supplier : EntityBase<int>
    {
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
