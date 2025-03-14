using CMSECommerce.Infrastructure;
using E_commercePlants.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commercePlants.Data.Components
{
    public class SmallCartViewComponent():ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            SmallCartViewModel smallCartViewModel;

            if (cart is null || cart.Count==0)
            {
                smallCartViewModel = null;
            }
            else
            {
                smallCartViewModel = new SmallCartViewModel
                {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount=cart.Sum(x=>x.Quantity*x.Price)
                };
            }

            return View(smallCartViewModel);
        }
    }
}
