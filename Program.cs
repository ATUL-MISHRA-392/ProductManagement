using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0))
    ));

var app = builder.Build();

// Ensure images directory exists in wwwroot for uploads
var imagesFolder = Path.Combine(app.Environment.WebRootPath, "images");
if (!Directory.Exists(imagesFolder))
{
    Directory.CreateDirectory(imagesFolder);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseAuthorization();

// Serve build-time static assets and dynamic runtime static files (uploaded images)
app.UseStaticFiles();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
