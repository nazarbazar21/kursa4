using InternetShopAspNet.Data;
using InternetShopAspNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAspNet.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public OrdersController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public IActionResult Checkout() => View();

    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var userId = _userManager.GetUserId(User)!;
        var cart = await _db.CartItems.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();
        if (!cart.Any()) return RedirectToAction("Index", "Cart");

        var order = new Order
        {
            UserId = userId,
            CustomerName = model.CustomerName,
            Phone = model.Phone,
            Address = model.Address,
            TotalPrice = cart.Sum(c => c.Product!.Price * c.Quantity),
            Items = cart.Select(c => new OrderItem
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Price = c.Product!.Price
            }).ToList()
        };

        _db.Orders.Add(order);
        _db.CartItems.RemoveRange(cart);
        await _db.SaveChangesAsync();
        return RedirectToAction("Success");
    }

    public IActionResult Success() => View();
}
