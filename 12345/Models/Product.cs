using System.ComponentModel.DataAnnotations;

namespace InternetShopAspNet.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Range(0.01, 999999)]
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = "/css/product-placeholder.svg";
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
