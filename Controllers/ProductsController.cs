using E_commercePlants.Data;
using E_commercePlants.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_commercePlants.Controllers
{

    public class ProductsController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;
        public async Task<IActionResult> Index(string slug, int page = 1)
        {

            ViewBag.CategorySlug = slug;
            int pageSize = 3;
            ViewBag.PageNumber = page;
            ViewBag.PageRange = pageSize;
     
            if (slug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Products.Count() / pageSize);

                List<Product> products = await _context.Products
                    .Include(x => x.Category).OrderByDescending(x => x.Id)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return View(products);

            }

            Category category = await _context.Categories.Where(x => x.Slug == slug).FirstOrDefaultAsync();
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var productsByCategory = _context.Products
            .Where(x => x.CategoryId == category.Id);

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsByCategory.Count() / pageSize);

            return View(await productsByCategory.Include(x => x.Category).OrderByDescending(x => x.Id)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync());

        }



    }
}
