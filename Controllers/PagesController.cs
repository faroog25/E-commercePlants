using E_commercePlants.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_commercePlants.Controllers
{
    public class PagesController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        public async Task<IActionResult> Index(string slug="")
        {
            slug=slug.IsNullOrEmpty()?"home":slug;
            Page page = await _context.Pages.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            if (page == null) { return NotFound(); }

            return View(page);
        }
    }
}
