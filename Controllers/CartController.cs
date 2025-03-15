using E_commercePlants.Data;
using E_commercePlants.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_commercePlants.Controllers
{
    public class CartController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

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
    }
}
