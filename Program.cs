using E_commercePlants.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"));
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
    option.Cookie.IsEssential = true;
}

);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequiredLength = 5;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;

    option.User.RequireUniqueEmail = true;

});

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "products",
    pattern: "products/product/{slug?}",
    defaults: new { controller = "Products", action = "Product" }
);

app.MapControllerRoute(
    name: "products",
    pattern: "products/{slug?}",
    defaults: new { controller = "Products", action = "Index" }
);

app.MapControllerRoute(
    name: "cart",
    pattern: "cart/{action}/{id?}",
    defaults: new { controller = "Cart", action = "Index" }
);

app.MapControllerRoute(
    name: "pages",
    pattern: "{slug?}",
    defaults:new { controller="Pages",action="Index"}
    
    );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
