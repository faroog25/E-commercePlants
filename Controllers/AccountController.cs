using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commercePlants.Controllers
{
    public class AccountController(UserManager<IdentityUser> userManager) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser newUser= new IdentityUser {
                    UserName=user.UserName,Email=user.Email,
                    PhoneNumber=user.Phone
                    
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
