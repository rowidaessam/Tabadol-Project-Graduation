using identityWithChristina.Models;
using identityWithChristina.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace identityWithChristina.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {

            userManager = _userManager;
            signInManager = _signInManager;
        }
        ApplicationUser us = new ApplicationUser();
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterAccountViewModel newAccount)
        {
            ApplicationUser us = new ApplicationUser();
            us.UserName = newAccount.UserName;
            us.Email = newAccount.Email;


            IdentityResult r = await userManager.CreateAsync(us, newAccount.password);
            if (r.Succeeded)
            {
                await signInManager.SignInAsync(us, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in r.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }

                return View(us);
            }




        }

        // log IN
        public IActionResult Login(string ReturnUrl="~/Home")
        {
            ViewData["redirectUrl"] = ReturnUrl;
            return View();
        }

        // assighn user to role 
        // await usermanager.AddToRoleAsync(user,"admin");

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel newLogging)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = await userManager.FindByNameAsync(newLogging.UserName);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult signInResult =
                        await signInManager.PasswordSignInAsync(user, newLogging.Password, newLogging.IsPresist, false);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "elpassword not matche");
                    }

                }
                else { ModelState.AddModelError("", "user name npt exisst"); }

            }


            return View("Login");
        }
        // log OUT
        public async Task<IActionResult> Logout()
        {
          await signInManager.SignOutAsync();
            return View("Login");
        }

        //addAdmin
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterAccountViewModel newAccount)
        {
            ApplicationUser us = new ApplicationUser();
            us.UserName = newAccount.UserName;
            us.Email = newAccount.Email;


            IdentityResult r = await userManager.CreateAsync(us, newAccount.password);
            if (r.Succeeded)
            {

                await userManager.AddToRoleAsync(us, "admin");
                await signInManager.SignInAsync(us, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in r.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }

                return View(us);
            }




        }


    }
}
