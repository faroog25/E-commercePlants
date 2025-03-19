using E_commercePlants.Data;
using E_commercePlants.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace E_commercePlants.Controllers
{
    public class OrdersController(AppDbContext context,SignInManager<ApplicationUser> signInManager) : Controller
    {
        private readonly AppDbContext _context = context;
        private readonly SignInManager<ApplicationUser> signInManager = signInManager;

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
             List<CartItem> cart = HttpContext.Session
                .GetJson<List<CartItem>>("Cart") ;
            Order order = new Order{
                Username=User.Identity.Name,GrandTotal=cart.Sum(x=>x.Price*x.Quantity)
            };
            _context.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in cart)
            {
                OrderDetail orderDetail=new()
                {
                    OrderId=order.Id,
                    ProductId=item.ProductId,
                    ProductName=item.ProductName,
                    Quantity=item.Quantity,
                    Price=item.Price,
                    Image=item.Image
                };

                _context.Add(orderDetail);
            await _context.SaveChangesAsync();

            }
            HttpContext.Session.Remove("Cart");

            return Redirect("/orders/index");
        }

    }
}
