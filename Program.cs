using BethanysPieShop.Models;

using Microsoft.EntityFrameworkCore;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

// CreateBuilder method - setup Kestrel server, configure IIS integration, specify root content, read application settings
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BethanysPieShopDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BethanysPieShopDbContextConnection' not found.");

// When serializing ignore the cycles of pies references categories categories referencing pies, etc...
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Register Razor Pages using extension method
builder.Services.AddRazorPages();


// Register repository interface and implementation
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IPieRepository, PieRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//Invoke GetCart method on the ShoppingCart class passing in the service provider; addscoped will create the shopping cart for the request
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<BethanysPieShopDbContext>(options => options.UseSqlServer(
        connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<BethanysPieShopDbContext>();

//Register Blazor
builder.Services.AddServerSideBlazor();

//Not needed since the addcontrollerwithviews is used
//builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}



app.UseStaticFiles();

//Bring in support for sessions with middleware
app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

//route default; each is considered a segments and can add as many as needed
//app.MapDefaultControllerRoute(); //Route Default: "{controller=Home}/{action=Index}/{id?}"
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map Razor Pages
app.MapRazorPages();

app.MapBlazorHub(); //Ready incoming connections from components
app.MapFallbackToPage("/app/{*catchall}", "/App/Index"); //when no match is found; will route to other Blazor components

//app.MapControllers();

//End of manually added middleware components

//Call dbInitializer
DbInitializer.Seed(app);

app.Run();
