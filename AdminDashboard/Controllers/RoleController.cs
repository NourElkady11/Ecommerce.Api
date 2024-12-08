using AdminDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> _roleManager)
        {
            this.roleManager = _roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }
       

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
           
            if (ModelState.IsValid)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleViewModel.Name);
                if (roleExist)
                {

                    ModelState.AddModelError("Name", "This Role is arleady exist");
                    return View(roleViewModel);
                }

                var result = await roleManager.CreateAsync(new IdentityRole { Name = roleViewModel.Name.Trim() });
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(roleViewModel);
                }


            }
            return RedirectToAction(nameof(Index));


        }

        


       
        public async Task<IActionResult> Edit(string id)
        {
            var RoleById = await roleManager.FindByIdAsync(id);
            var MappedRole = new RoleViewModel()
            {
                Name = RoleById.Name,
                Id=RoleById.Id
            };
            return View(MappedRole);

        }



        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleViewModel.Name);
                if (roleExist)
                {

                    ModelState.AddModelError("Name", "This Role is arleady exist");
                    return View(roleViewModel);
                }
                else
                {
                    var role = await roleManager.FindByIdAsync(roleViewModel.Id);
                    if (role is not null)
                    {
                        role.Name = roleViewModel.Name;
                        await roleManager.UpdateAsync(role);
                    }
                    else
                    {
                        ModelState.AddModelError("", "This Role is Not Found");
                        return View(roleViewModel);
                    }
                }
                    return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }



        public async Task<IActionResult> Delete(string? id)
        {
            var getRoleById=await roleManager.FindByIdAsync(id);
            if(getRoleById is not null)
            {
              await roleManager.DeleteAsync(getRoleById);

            }
            return RedirectToAction("Index");
        }



    }
}
