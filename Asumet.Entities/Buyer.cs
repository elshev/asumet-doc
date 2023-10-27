﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asumet.Entities
{
    public class Buyer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
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