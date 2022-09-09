using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Models
{
    public class Certification
    {
        [Key]
        public int certificationId { get; set; }
        [Required]
        [MaxLength(8)]
        public string sigle { get; set; }
        [Required]
        [MinLength(5)]
        public string libelle { get; set; }
        public ICollection<StudentCertification> StudentsLink { get; set; }
    }
}
