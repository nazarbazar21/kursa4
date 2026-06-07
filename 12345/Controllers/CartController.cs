using InternetShopAspNet.Data;
using InternetShopAspNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAspNet.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public CartController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User)!;
        var items = await _db.CartItems.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();
        return View(items);
    }

    public async Task<IActionResult> Add(int productId)
    {
        var userId = _userManager.GetUserId(User)!;
        var item = await _db.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        if (item == null) _db.CartItems.Add(new CartItem { UserId = userId, ProductId = productId, Quantity = 1 });
        else item.Quantity++;
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int id)
    {
        var item = await _db.CartItems.FindAsync(id);
        if (item != null) _db.CartItems.Remove(item);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
