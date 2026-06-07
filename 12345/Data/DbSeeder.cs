using InternetShopAspNet.Models;

namespace InternetShopAspNet.Data;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext db)
    {
        if (db.Categories.Any()) return;

        var categories = new List<Category>
        {
            new() { Name = "Смартфони" },
            new() { Name = "Ноутбуки" },
            new() { Name = "Аксесуари" }
        };
        db.Categories.AddRange(categories);
        db.SaveChanges();

        db.Products.AddRange(
            new Product { Name = "SmartPhone X10", Description = "Сучасний смартфон із якісною камерою.", Price = 15999, CategoryId = categories[0].Id },
            new Product { Name = "Laptop Pro 15", Description = "Ноутбук для навчання, роботи та розваг.", Price = 32999, CategoryId = categories[1].Id },
            new Product { Name = "Wireless Mouse", Description = "Зручна бездротова миша.", Price = 699, CategoryId = categories[2].Id },
            new Product { Name = "Headphones Orange", Description = "Навушники з чистим звуком.", Price = 1299, CategoryId = categories[2].Id }
        );
        db.SaveChanges();
    }
}
