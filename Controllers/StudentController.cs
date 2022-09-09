using certificationRegister.Models;
using certificationRegister.Repositories;
using certificationRegister.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepository<Student> _bd;
        private readonly IRepository<Certification> _ct;
        private readonly AppDbcontext _app;

        public StudentController(IRepository<Student> bd, IRepository<Certification> ct, AppDbcontext app)
        {
            _bd = bd;
            _ct = ct;
            _app = app;
        }
        public IActionResult Index()
        {
            var students = _bd.AllCertification();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var std = new Student()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Country = model.Country,
                };
                _bd.Create(std);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                var st = _bd.GetCertification(id);
                if (st != null)
                {
                    var model = new StudentEditViewModel()
                    {
                        studentId = st.studentId,
                        FirstName = st.FirstName,
                        LastName = st.LastName,
                        Country = st.Country,
                    };
                    return View(model);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var st = _bd.GetCertification(model.studentId);
                if (st != null)
                {
                    st.FirstName = model.FirstName;
                    st.LastName = model.LastName;
                    st.Country = model.Country;
                    _bd.Update(st);
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Changer(StudentChangeStatusViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var st = _bd.GetCertification(model.studentId);
        //        if (st != null)
        //        {
        //            st.status = model.status;
        //            _bd.Update(st);
        //        }
                
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var st = _bd.GetCertification(id);
            if (st != null)
            {

                List<StudentAddCertificationViewModel> models = new List<StudentAddCertificationViewModel>();
                var certifications = _ct.AllCertification();
                foreach (Certification cert in certifications)
                {
                    StudentAddCertificationViewModel model = new StudentAddCertificationViewModel()
                    {
                        CertificationId = cert.certificationId,
                        libelle = cert.libelle,
                        sigle = cert.sigle,
                        IsSelected = false
                    };

                    if (IsJoin(model.CertificationId, st.studentId))
                    {
                        model.IsSelected = true;
                    }
                    models.Add(model);
                }
                ViewBag.student = st.FirstName + " " + st.LastName;
                ViewBag.studentId = st.studentId;
                return View(models);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Detail(List<StudentAddCertificationViewModel> model, int studentId)
        {
            var student = await _app.students.Include(s => s.CertificationsLink)
                .SingleAsync(s => s.studentId == studentId);
            if (student != null)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    var cert = await _app.certifications.Include(c => c.StudentsLink)
                        .SingleAsync(c => c.certificationId == model[i].CertificationId);

                    //recuperer les donnees de la table ternaire
                    var AddOrRemove = new StudentCertification()
                    {
                        Student = student,
                        Certification = cert,
                        DateCertified = model[i].dte,
                        CertId = model[i].CertificationId,
                        StudId = studentId,
                        IsActive = true
                    };

                    //Detacher la certification a l'etudiant
                    if (IsJoin(model[i].CertificationId, studentId) && !model[i].IsSelected)
                    {
                        student.CertificationsLink.Remove(AddOrRemove);
                        await _app.SaveChangesAsync();
                    }//Joindre la certification a l'etudiant
                    else if (!IsJoin(model[i].CertificationId, studentId) && model[i].IsSelected)
                    {
                        student.CertificationsLink.Add(AddOrRemove);
                        await _app.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        private bool IsJoin(int certId, int studId)
        {
            var result = _app.certifications.Where(c => c.certificationId == certId)
                 .Include(c => c.StudentsLink)
                 .Where(c => c.StudentsLink.Any(s => s.StudId == studId))
                 .FirstOrDefault();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
