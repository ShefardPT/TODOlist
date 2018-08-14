using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TDList.API.Controllers
{
    // Account controller
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<Entities.AppUser> _userManager;
        private SignInManager<Entities.AppUser> _signInManager;
        private RoleManager<Entities.AppUser> _roleManager;

        public AccountController(UserManager<Entities.AppUser> userManager, 
                                 SignInManager<Entities.AppUser> signInManager
                                 RoleManager<Entities.AppUser> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Login methods
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return null;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Models.LoginViewModel loginViewModel, string returnUrl = null)
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {

        }

        // Register methods
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            return null;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Models.RegisterViewModel, string returnUrl = null)
        {
            return null;
        }


    }
}