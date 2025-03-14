using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commercePlants.Data.Components
{
    public class CategoriesViewComponent(AppDbContext context):ViewComponent
    {
        private readonly AppDbContext _context=context;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Categories.ToListAsync());
        }
    }
}
