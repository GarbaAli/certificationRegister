using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Models
{
    public class Student
    {
        [Key]
        public int studentId { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        public ICollection<StudentCertification> CertificationsLink { get; set; }

    }
}
