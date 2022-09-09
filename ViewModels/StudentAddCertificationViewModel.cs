using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.ViewModels
{
    public class StudentAddCertificationViewModel
    {
        public int CertificationId { get; set; }
        public string libelle { get; set; }
        [Required]
        [Display(Name = "certification Date ")]
        public DateTime dte { get; set; }
        public bool IsSelected { get; set; }
        public string sigle { get; set; }
    }
}
