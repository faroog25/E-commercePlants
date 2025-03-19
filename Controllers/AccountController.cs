using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_commercePlants.Models;

namespace E_commercePlants.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser= new ApplicationUser {
                    UserName=user.UserName,Email=user.Email,
                    PhoneNumber=user.Phone,
                    Address=user.Address
                    
                };
                IdentityResult result = await _userManager.CreateAsync(newUser
                ,user.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser ,"Customer");
                    Redirect("/");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }

            }
            return View(user);
        }
    }
}
