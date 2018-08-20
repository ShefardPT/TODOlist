using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TODOList.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<Entities.AppUser> _userManager;
        private RoleManager<Entities.AppUser> _roleManager;
        private SignInManager<Entities.AppUser> _signInManager;

        public AccountController(UserManager<Entities.AppUser> userManager,
                                    SignInManager<Entities.AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Models.RegisterView model)
        {
            if (ModelState.IsValid)
            {
                Entities.AppUser user = new Entities.AppUser { UserName = model.Username, Email = model.Email};
                // User addition
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Cookie setup
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}