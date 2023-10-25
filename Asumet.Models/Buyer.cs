using System.ComponentModel.DataAnnotations;

namespace Asumet.Models
{
    public class Buyer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Bank { get; set; }

        public string Bic { get; set; }

        public string CorrespondentAccount { get; set; }

        public string Account { get; set; }

        public string ResponsiblePerson { get; set; }
    }
}