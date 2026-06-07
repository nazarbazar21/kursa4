using System.ComponentModel.DataAnnotations;

namespace InternetShopAspNet.Models;

public class CheckoutViewModel
{
    [Required]
    public string CustomerName { get; set; } = string.Empty;
    [Required]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
}
