using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_commercePlants.Models;
using E_commercePlants.Models.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace E_commercePlants.Controllers
{
    public class AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

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
                    TempData["success"]="You have registered successfully";

                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(user);
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel {ReturnUrl =returnUrl});
        }
         [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                SignInResult signInResult =await _signInManager
                    .PasswordSignInAsync(loginViewModel.UserName,loginViewModel.Password,false,false);
                if (signInResult.Succeeded)
                {
                    return Redirect(loginViewModel.ReturnUrl??"/");
                }
                ModelState.AddModelError("","Invalid username or password");
            }
             
            return View(loginViewModel);
        }
        public async Task<IActionResult> Logout()
        {
            await  _signInManager?.SignOutAsync();
            return Redirect("/");
        }
        
    }
}
