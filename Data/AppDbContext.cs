using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using E_commercePlants.Models;

namespace E_commercePlants.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Shirts", Slug = "shirts" },
               new Category { Id = 2, Name = "Fruit", Slug = "fruit" }
           );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Apples",
                    Slug = "apples",
                    Description = "Juicy apples",
                    Price = 1.50M,
                    CategoryId = 2,
                    Image = "apple1.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Grapefruit",
                    Slug = "grapefruit",
                    Description = "Juicy grapefruit",
                    Price = 2M,
                    CategoryId = 2,
                    Image = "grapefruit1.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Grapes",
                    Slug = "grapes",
                    Description = "Fresh grapes",
                    Price = 1.80M,
                    CategoryId = 2,
                    Image = "grapes1.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Oranges",
                    Slug = "oranges",
                    Description = "Fresh oranges",
                    Price = 1.50M,
                    CategoryId = 2,
                    Image = "orange1.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "Blue shirt",
                    Slug = "blue-shirt",
                    Description = "Nice blue t-shirt",
                    Price = 7.99M,
                    CategoryId = 1,
                    Image = "blue1.jpg"
                },
                new Product
                {
                    Id = 6,
                    Name = "Red shirt",
                    Slug = "red-shirt",
                    Description = "Nice red t-shirt",
                    Price = 8.99M,
                    CategoryId = 1,
                    Image = "red1.jpg"
                },
                new Product
                {
                    Id = 7,
                    Name = "Green shirt",
                    Slug = "green-shirt",
                    Description = "Nice green t-shirt",
                    Price = 9.99M,
                    CategoryId = 1,
                    Image = "green1.png"
                },
                new Product
                {
                    Id = 8,
                    Name = "Pink shirt",
                    Slug = "pink-shirt",
                    Description = "Nice pink t-shirt",
                    Price = 10.99M,
                    CategoryId = 1,
                    Image = "pink1.png"
                }
            );

            modelBuilder.Entity<Page>().HasData(

                new Page { Id = 1, Title = "Home", Slug = "home", Body = "This is the home page" },
                new Page { Id = 2, Title = "About", Slug = "about", Body = "This is the about page" },
                new Page { Id = 3, Title = "Services", Slug = "services", Body = "This is the services page" },
                new Page { Id = 4, Title = "Contact", Slug = "contact", Body = "This is the contact page" }

                );
        }
    }
}
