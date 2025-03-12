﻿using E_commercePlants.Data;
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

        public async Task<IActionResult> Index()
        {

            List<Product> products = await _context.Products.Include(x=>x.Category).OrderByDescending(x=>x.Id).ToListAsync();


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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

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
                if (product.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);

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

    }
}
