using certificationRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Repositories
{
    public class CertificationRepository : IRepository<Certification>
    {
        private readonly AppDbcontext _context;

        public CertificationRepository(AppDbcontext context)
        {
            _context = context;
        }
        public IEnumerable<Certification> AllCertification()
        {
           return _context.certifications.ToList();
        }

        public void Create(Certification t)
        {
            _context.certifications.Add(t);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
           var model =  _context.certifications.Find(id);
            if (model != null)
            {
                _context.certifications.Remove(model);
                _context.SaveChanges();
            }
        }

        public Certification GetCertification(int id)
        {
          return  _context.certifications.Find(id);
        }

        public void Update(Certification t)
        {
            _context.certifications.Update(t);
            _context.SaveChanges();
        }
    }
}
