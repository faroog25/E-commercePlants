using E_commercePlants.Data;
using E_commercePlants.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_commercePlants.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController(AppDbContext context,IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly AppDbContext _context = context;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<IActionResult> Index(int categoryId)
        {
            ViewBag.Categories=new SelectList(_context.Categories,"Id","Name",categoryId.ToString()); 

            List<Product> products = await _context.Products
                .Where(x=>categoryId ==0 || x.CategoryId==categoryId)
                .Include(x=>x.Category).OrderByDescending(x=>x.Id).ToListAsync();


            return View(products);
        }

        public IActionResult Create()
        { 
            ViewBag.Categories=new SelectList(_context.Categories,"Id","Name"); 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name",product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");
                var slug = await _context.Products.FirstOrDefaultAsync(x => x.Slug == product.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "That product already exists!");
                    return View(product);
                }


                string imageName;
                if (product.ImageUpload!=null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    imageName=Guid.NewGuid().ToString()+"_"+product.ImageUpload.FileName;
                    string filePath=Path.Combine(uploadDir, imageName);

                    FileStream fs=new FileStream(filePath, FileMode.Create);

                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                }
                else
                {
                    imageName = "noimage.png";
                }

                product.Image = imageName;
                        
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The product has been added!";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product is null) { return NotFound(); }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name",product.CategoryId);

            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/gallery/"
                + id.ToString());

            if (Directory.Exists(uploadDir))
            {
                product.GalleryImages = Directory.EnumerateFiles(uploadDir).Select(
                    x=>Path.GetFileName(x));
            }
            return View(product);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");
                var slug = await _context.Products.Where(p => p.Id != product.Id).FirstOrDefaultAsync(x => x.Slug == product.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "That product already exists!");
                    return View(product);
                }


                
                if (product.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");

                    if (!string.Equals(product.Image, "noimage.png"))
                    {
                        string oldImagePath= Path.Combine(uploadDir, product.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    string  imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new(filePath, FileMode.Create);

                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    product.Image = imageName;

                }
              

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The product has been updated!";
                return RedirectToAction("Edit",new {product.Id});
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null) { 
                TempData["error"]="The product does not exist!";
            }
            else {
                if (!string.Equals(product.Image, "noimage.png"))
                {
                    string productImage = Path.Combine(_webHostEnvironment.WebRootPath, "media/products/" + product.Image);

                    if (System.IO.File.Exists(productImage))
                    {
                        System.IO.File.Delete(productImage);
                    }
                }
                string gallery = Path.Combine(
                    _webHostEnvironment.WebRootPath, "media/gallery/"
                    + product.Id.ToString());

                if (Directory.Exists(gallery))
                {
                    Directory.Delete(gallery);
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The product has been deleted!";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UploadImages(int id)
        {
            var files = HttpContext.Request.Form.Files;

            if (files.Any())
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/gallery/"+id.ToString());
                

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                foreach (var file in files)
                {
                    string imageName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new(filePath, FileMode.Create);

                    await file.CopyToAsync(fs);
                    fs.Close();
                }

                return Ok();
            }
            return View();
        }
        [HttpPost]
        public void DeleteImage(int id , string imageName)
        {
            string fallPath=Path.Combine(_webHostEnvironment.WebRootPath,
             "media/gallery/"+id.ToString()+"/"+imageName);

             if(System.IO.File.Exists(fallPath))
             {
                System.IO.File.Delete(fallPath);
             }
        }

    }
}
