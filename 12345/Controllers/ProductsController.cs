using InternetShopAspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAspNet.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _db;
    public ProductsController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index(int? categoryId, string? search)
    {
        ViewBag.Categories = await _db.Categories.ToListAsync();
        var products = _db.Products.Include(p => p.Category).AsQueryable();

        if (categoryId.HasValue) products = products.Where(p => p.CategoryId == categoryId);
        if (!string.IsNullOrWhiteSpace(search)) products = products.Where(p => p.Name.Contains(search));

        return View(await products.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? NotFound() : View(product);
    }
}
