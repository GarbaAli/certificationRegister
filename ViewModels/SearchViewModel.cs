using certificationRegister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        [Display(Name ="Id Certification")]
        public int certId { get; set; }
        [Required]
        [Display(Name = "Fist Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
   
    }
}
