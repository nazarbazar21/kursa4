using Microsoft.AspNetCore.Mvc;

namespace InternetShopAspNet.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
