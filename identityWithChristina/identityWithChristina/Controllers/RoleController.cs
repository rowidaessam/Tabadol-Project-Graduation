using identityWithChristina.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace identityWithChristina.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> _roleManager) {
            roleManager = _roleManager;
        }


        public IActionResult AddRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel newrole)
        {
            IdentityRole role =new IdentityRole();
            
                role.Name=newrole.Name;
         IdentityResult result=  await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return View();

            }
            else {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
            }

           
            return View();
        }

    }
}
