using certificationRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.ViewModels
{
    public class certViewModel:Certification
    {
        public DateTime dte { get; set; }
        public string renouveller { get; set; }
    }
}
