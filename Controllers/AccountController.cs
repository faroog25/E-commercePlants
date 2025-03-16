using Microsoft.AspNetCore.Mvc;

namespace E_commercePlants.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
