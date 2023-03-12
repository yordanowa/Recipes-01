using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.Data.Models;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public AdministrationController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(InputRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var role = new ApplicationRole { Name = model.RoleName };
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Redirect("/");
            }
            else
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            return View(model);
        }
    }
}
