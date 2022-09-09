using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Models
{
    public class StudentCertification
    {
        public int StudId { get; set; }
        public int CertId { get; set; }
        public DateTime DateCertified { get; set; }
        public bool IsActive { get; set; }
        public Student Student { get; set; }
        public Certification Certification { get; set; }
    }
}
