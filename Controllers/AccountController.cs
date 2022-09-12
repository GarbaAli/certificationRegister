using certificationRegister.Models;
using certificationRegister.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace certificationRegister.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    //UserName = GenerateUserName(model.FirstName, model.LastName),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToAction("Search", "Home");
                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }
                }
                ModelState.AddModelError(string.Empty, "Login Invalid Attempt");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Search", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                AppUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    AccountEditViewModel model = new AccountEditViewModel()
                    {
                        FirstName = user.FirstName,
                        Email = user.Email,
                        LastName = user.LastName,
                        Password = user.PasswordHash,
                        ConfirmPassword = user.PasswordHash
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Contact");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        //Acher le password
                        var passwordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                        if (passwordHash != user.PasswordHash)
                        {
                            user.FirstName = model.FirstName;
                            user.LastName = model.LastName;
                            user.Email = model.Email;
                            user.PasswordHash = passwordHash;
                        }

                    }
                    else
                    {
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Email = model.Email;
                    }


                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Search", "Home");
                    }

                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }


            }

            return View(model);
        }

    }
}
