using certificationRegister.Models;
using certificationRegister.Repositories;
using certificationRegister.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbcontext _bd;

        public HomeController(ILogger<HomeController> logger, AppDbcontext bd)
        {
            _logger = logger;
           _bd = bd;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string first, string last, int ID, int token)
        {
            if (token != 0 && (ID == 0 && last==null && first==null) )
            {
                ViewBag.result = "error";
                return View();
            }

            // si c'est last et first sont les parametres de recherches
            if (!string.IsNullOrEmpty(last) || !string.IsNullOrEmpty(first) && ID == 0)
            {
                List<ResultSearchViewModel> models = new List<ResultSearchViewModel>();
                var result = await _bd.students.Where(s => s.FirstName.Contains(first) || s.LastName.Contains(last) ||  s.FirstName.Contains(last) || s.LastName.Contains(first))
                    .Include(s => s.CertificationsLink).ThenInclude(s => s.Certification)
                    .ToListAsync();
                if (result.Count != 0)
                {
                    for (int i = 0; i < result.Count(); i++)
                    {
                        foreach (var item in result[i].CertificationsLink)
                        {
                            var data = new ResultSearchViewModel()
                            {
                                sigle = item.Certification.sigle,
                                libelle = item.Certification.libelle,
                                status = item.IsActive,
                                Date = item.DateCertified,
                                Country = result[i].Country,
                                Name = result[i].FirstName+" "+ result[i].LastName
                            };
                            models.Add(data);
                        }
                    }
                    ViewBag.result = "second method";
                    return View(models);
                }
                else
                {
                    ViewBag.result = "empty";
                    return View();
                }
            }

               // si c'est ID le parametre de recherche
            if (string.IsNullOrEmpty(last) && string.IsNullOrEmpty(first) && ID != 0)
            {
                List<ResultSearchViewModel> models = new List<ResultSearchViewModel>();
                var result = await _bd.certifications.Where(c => c.certificationId == ID)
                   .Include(c => c.StudentsLink).ThenInclude(c => c.Student)
                    .ToListAsync();
                    
                if (result.Count != 0)
                {
                    var tempo = await _bd.certifications.FindAsync(ID);
                    if (tempo != null)
                    {
                        ViewBag.libelle = tempo.libelle;
                    }
                    
                    foreach (var item in result[0].StudentsLink)
                    {
                        var data = new ResultSearchViewModel()
                        {
                         Name = item.Student.FirstName+" "+ item.Student.LastName,
                         Country = item.Student.Country,
                         Date = item.DateCertified,
                         status = item.IsActive
                        };
                        models.Add(data);
                    }
                    
                    ViewBag.result = "data";
                    return View(models);
                }
                else
                {
                    ViewBag.libelle = "";
                    ViewBag.result = "empty";
                    return View();
                }
            }

            ViewBag.result = "no";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
