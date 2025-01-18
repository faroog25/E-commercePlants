using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commercePlants.Data.Components
{
    public class MenuViewComponent(AppDbContext context):ViewComponent
    {
        private readonly AppDbContext _context=context;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Pages.Where(x=> x.Slug!="home").ToListAsync());
        }
    }
}
