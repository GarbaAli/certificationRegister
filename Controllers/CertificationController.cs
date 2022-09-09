using certificationRegister.Models;
using certificationRegister.Repositories;
using certificationRegister.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Controllers
{
    public class CertificationController : Controller
    {
        private readonly IRepository<Certification> _bd;

        public CertificationController(IRepository<Certification> certification)
        {
            _bd = certification;
        }
        public IActionResult Index()
        {
            var model = _bd.AllCertification();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CertificationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cert = new Certification()
                {
                    sigle = model.sigle.ToUpper(),
                    libelle = model.libelle
                };
                _bd.Create(cert);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _bd.GetCertification(id);
            if (model != null)
            {
                var cert = new CertificationEditViewModel()
                {
                    sigle = model.sigle,
                    libelle = model.libelle,
                    certificationId = model.certificationId
                };
                return View(cert);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult Edit(CertificationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cert = _bd.GetCertification(model.certificationId);
                if (cert != null)
                {
                    cert.sigle = model.sigle;
                    cert.libelle = model.libelle;
                    _bd.Update(cert);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _bd.GetCertification(id);
            if (model != null)
            {
                var cert = new CertificationEditViewModel()
                {
                    sigle = model.sigle,
                    libelle = model.libelle,
                    certificationId = model.certificationId
                };
                return View(cert);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(CertificationEditViewModel model)
        {
            if (model.certificationId != 0)
            {
                var cert = _bd.GetCertification(model.certificationId);
                if (cert != null)
                {
                    _bd.Delete(model.certificationId);
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(model);
        }
    }
}
