using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.ViewModels
{
    public class CertificationEditViewModel
    {
        public int certificationId { get; set; }
        [Required]
        [MaxLength(8)]
        public string sigle { get; set; }
        [Required]
        [MinLength(5)]
        public string libelle { get; set; }
    }
}
