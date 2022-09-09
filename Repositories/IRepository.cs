using certificationRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> AllCertification();
        T GetCertification(int id);
        void Create(T t);
        void Update(T t);
        void Delete(int id);
    }
}
