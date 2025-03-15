using E_commercePlants.Data;
using E_commercePlants.Helpers;
using E_commercePlants.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_commercePlants.Controllers
{
    public class CartController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session
                .GetJson<List<CartItem>>("Cart") ?? [];
            CartViewModel cartViewModel = new()
            {
                CartItems = cart,

                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(cartViewModel);
        }
        public async Task<IActionResult> Add(int id)
        {
           
            Product product = await _context.Products.FindAsync(id);

            if (product == null) { return NotFound(); }

            List<CartItem> cart=HttpContext.Session
                .GetJson<List<CartItem>>("Cart") ?? [];
            CartItem cartItem=cart.Where(c=>c.ProductId==id).FirstOrDefault();

            if(cartItem==null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity++;
            }
            HttpContext.Session.SetJson("Cart",cart);

            TempData["success"]="The product has been added!";

            return Redirect(Request.Headers.Referer.ToString());
        }

        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session
                .GetJson<List<CartItem>>("Cart") ?? [];
            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();
            
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else // quantity is 1
                {
                    cart.RemoveAll(c => c.ProductId == id); // remove item completely
                }
            }
            
            HttpContext.Session.SetJson("Cart", cart);
            TempData["success"] = "The product quantity has been decreased!";
            return RedirectToAction(nameof(Index));
        }
        public  IActionResult Remove(int id)
        {
            List<CartItem> cart=HttpContext.Session
                .GetJson<List<CartItem>>("Cart") ?? [];
            cart.RemoveAll(c=>c.ProductId==id);
            HttpContext.Session.SetJson("Cart",cart);
            TempData["success"]="The product has been removed!";
            return RedirectToAction(nameof(Index)); 

        }
        public  IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            TempData["success"]="The cart has been cleared!";
            return RedirectToAction(nameof(Index));
        }
        
    }
}
