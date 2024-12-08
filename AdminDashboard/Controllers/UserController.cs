using AdminDashboard.Models;
using AdminDashboard.Models.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace AdminDashboard.Controllers
{
    public class UserController(UserManager<User> userManager,RoleManager<IdentityRole> roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();

            var mappedUser=users.Select(u=>new UserViewModel() {
                Id = u.Id,
                FirstName =u.FirstName,
                LastName=u.LastName,
                Username = u.UserName,
                Email =u.Email,
                PhoneNumber=u.PhoneNumber,
                Roles=userManager.GetRolesAsync(u).Result
              
            }).ToList();
            
            return View(mappedUser);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var allroles=roleManager.Roles.ToList();

            var mappedUser = new UserRoleViewModel()
            {

                Id = user.Id,
                UserName = user.UserName,
                Roles = allroles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList()


            };
            return View(mappedUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel userRoleViewModel)
        {
            var user = await userManager.FindByIdAsync(userRoleViewModel.Id);
            var rolesOfUser = await userManager.GetRolesAsync(user);

            try
            {
                foreach (var role in userRoleViewModel.Roles)
                {

                    if (rolesOfUser.Any(r => r == role.Name) && !role.IsSelected)
                    {
                        await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    if (!rolesOfUser.Any(r => r == role.Name) && role.IsSelected)
                    {
                        await userManager.AddToRoleAsync(user, role.Name);

                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex) {

                ModelState.AddModelError(string.Empty, ex.Message);

            }

            return View(userRoleViewModel);

        }






















    }
}
