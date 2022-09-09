//using certificationRegister.Models;
//using certificationRegister.ViewModels;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace certificationRegister.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<AppUser> _userManager;
//        private readonly SignInManager<AppUser> _signManager;

//        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
//        {
//            _userManager = userManager;
//            _signManager = signManager;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(AccountRegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                AppUser user = new AppUser
//                {
//                    //UserName = GenerateUserName(model.FirstName, model.LastName),
//                    FirstName = model.FirstName,
//                    LastName = model.LastName,
//                    UserName = model.Email,
//                    Email = model.Email
//                };
//                var result = await _userManager.CreateAsync(user, model.Password);

//                if (result.Succeeded)
//                {
//                    await _signManager.SignInAsync(user, isPersistent: false);
//                    return RedirectToAction("Index", "Account");
//                }

//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError("", error.Description);
//                }
//            }
//            return View(model);
//        }
//    }
//}
