using certificationRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly AppDbcontext _context;

        public StudentRepository(AppDbcontext appDbcontext)
        {
            _context = appDbcontext;
        }
        public IEnumerable<Student> AllCertification()
        {
            return _context.students.ToList();
        }

        public void Create(Student t)
        {
            _context.students.Add(t);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var st = GetCertification(id);
            if (st != null)
            {
                _context.students.Remove(st);
                _context.SaveChanges();
            }
        }

        public Student GetCertification(int id)
        {
            return  _context.students.Find(id);
            
        }

        public void Update(Student t)
        {
            _context.students.Update(t);
            _context.SaveChanges();
        }
    }
}
