using E_commercePlants.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_commercePlants.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]

    public class OrdersController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        public async Task<IActionResult> Index()
        {

            List<Order> orders = await _context.Orders.OrderByDescending(x=>x.Id).ToListAsync();


            return View(orders);
        }
       
       [HttpPost]
        public async Task<IActionResult> ShippedStatus(int id,bool shipped)
        {
            Order order=await _context.Orders.FirstOrDefaultAsync(x=>x.Id==id);

            order.Shipped=shipped;
            _context.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
    }
}
