
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(
        ApplicationDbContext context,
        IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // Display all products
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .OrderByDescending(p => p.Id)
            .ToListAsync();

        return View(products);
    }

    // Show Add Product form
    public IActionResult Create()
    {
        return View();
    }

    // Save new product
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        Product product, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
            return View(product);

        if (imageFile != null && imageFile.Length > 0)
        {
            var extension =
                Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(
                    "imageFile", "Only JPG, PNG and WEBP images are allowed.");
                return View(product);
            }

            if (imageFile.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError(
                    "imageFile", "Image size must be 5 MB or less.");
                return View(product);
            }

            var folder = Path.Combine(
                _environment.WebRootPath, "images");

            Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            product.ImagePath = "/images/" + fileName;
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Show Edit form
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    // Update existing product
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id, Product input, IFormFile? imageFile)
    {
        if (id != input.Id)
            return NotFound();

        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        if (!ModelState.IsValid)
            return View(input);

        product.Name = input.Name;
        product.Price = input.Price;
        product.Category = input.Category;
        product.Stock = input.Stock;
        product.Description = input.Description;

        if (imageFile != null && imageFile.Length > 0)
        {
            var extension =
                Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExtensions.Contains(extension) ||
                imageFile.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError(
                    "imageFile",
                    "Choose a JPG, PNG or WEBP image up to 5 MB.");

                return View(input);
            }

            var folder = Path.Combine(
                _environment.WebRootPath, "images");

            Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            // Remove previous uploaded image
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                var oldPath = Path.Combine(
                    _environment.WebRootPath,
                    product.ImagePath.TrimStart('/')
                        .Replace('/', Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            product.ImagePath = "/images/" + fileName;
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Show Delete confirmation
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    // Delete product
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product != null)
        {
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                var oldPath = Path.Combine(
                    _environment.WebRootPath,
                    product.ImagePath.TrimStart('/')
                        .Replace('/', Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
