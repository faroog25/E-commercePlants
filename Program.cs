using E_commercePlants.Data;
using E_commercePlants.Models;
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

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
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

builder.Services.AddAuthorization(option=>
{
    option.AddPolicy("Admin",policy => policy.RequireRole("Admin"));
    option.AddPolicy("Customer",policy => policy.RequireRole("Customer"));
});

var app = builder.Build();

//seed roles
// using (var scope =app.Services.CreateScope())
// {
//     var roleManger=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//     var roles=new []{"Admin","Customer"};
//     foreach (var role in roles)
//     {
//         if (!await roleManger.RoleExistsAsync(role))
//         {
//             await roleManger.CreateAsync(new IdentityRole(role));
//         }
//     }
// }

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

app.UseAuthentication();
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
    name: "orders",
    pattern: "orders/{action}",
    defaults: new { controller = "Orders", action = "Index" }
);

app.MapControllerRoute(
    name: "account",
    pattern: "account/{action}",
    defaults:new { controller="Account",action="Index"}
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
