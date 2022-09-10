using certificationRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.ViewModels
{
    public class ResultSearchViewModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public IEnumerable<StudentCertification> certification { get; set; }
        public DateTime Date { get; set; }
        public bool status { get; set; }
        public string sigle { get; set; }
        public string libelle { get; set; }
    }
}
