using System.ComponentModel.DataAnnotations;

namespace Asumet.Models
{
    /// <summary> Supplier stub </summary>
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        /// <summary> Full name </summary>
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Passport { get; set; }
    }
}
