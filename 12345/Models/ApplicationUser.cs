using Microsoft.AspNetCore.Identity;

namespace InternetShopAspNet.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
