using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace AdminDashboard.Controllers
{
    public class AdminController(UserManager<User> userManager, SignInManager<User> signInManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Errror Occured");
                return View(loginDto);
            }
            else
            {
                var user = await userManager.FindByEmailAsync(loginDto.Email);
                if (user != null)
                {
                    var res = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                    if (res.Succeeded && await userManager.IsInRoleAsync(user, "Admin") || await userManager.IsInRoleAsync(user, "SuperAdmin"))
                    {
                        await signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "I'm Really Sorry You are Not one of the Admins ya hacker yabnl kalbbb");
                        return View(loginDto);

                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect Email Or Password");
                    return View(loginDto);
                }
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }


    }
}
