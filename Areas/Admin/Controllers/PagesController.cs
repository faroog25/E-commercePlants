using E_commercePlants.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_commercePlants.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        public async Task<IActionResult> Index()
        {

            List<Page> pages = await _context.Pages.OrderBy(x=>x.Order).ToListAsync();


            return View(pages);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                var slug = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "That page already exists!");
                    return View(page);
                }

                page.Order = 100;

                _context.Pages.Add(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The page has been added!";
                return RedirectToAction("Index");
            }
            return View(page);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Page page = await _context.Pages.FindAsync(id);

            if (page == null)
                return NotFound();

            return View(page);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");

                var slug = await _context.Pages.FirstOrDefaultAsync(
                    x => x.Slug == page.Slug && x.Id != page.Id);

                if (slug != null)
                {
                    ModelState.AddModelError("", "That page already exists!");
                    return View(page);
                }

                _context.Pages.Update(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The page has been edited!";

                return RedirectToAction("Index");
            }
            return View(page);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Page page = await _context.Pages.FindAsync(id);

            if (page == null || id == 1)
                TempData["Error"] = "The page does not exist!";
            else
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");

        }

        public void ReorderPages(int[] id)
        {
            int count = 1;

            foreach (var pageId in id)
            {
                var page=_context.Pages.Find(pageId);
                page.Order = count;

                _context.SaveChanges();
                count++;
            }
        }
    }
}
