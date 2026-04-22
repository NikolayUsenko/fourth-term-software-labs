using fourth_term_software_labs.Models;

namespace fourth_term_software_labs.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            // Если в базе уже есть данные, пропускаем инициализацию
            if (context.Products.Any())
            {
                return;
            }
            // Добавляем тестовые товары
            var products = new Product[]
            {
                new Product
                {
                    Name = "Ноутбук ASUS",
                    Price = 75000,
                    Category = "Электроника",
                    Description = "Игровой ноутбук с RTX 3060",
                    CreatedDate = DateTime.Now.AddDays(-30),
                    InStock = true
                },
                new Product
                {
                    Name = "Смартфон Samsung",
                    Price = 45000,
                    Category = "Электроника",
                    Description = "Galaxy S23 256GB",
                    CreatedDate = DateTime.Now.AddDays(-15),
                    InStock = true
                },
                new Product
                {
                    Name = "Книга 'Изучаем C'",
                    Price = 1200,
                    Category = "Книги",
                    Description = "Подробное руководство по C",
                    CreatedDate = DateTime.Now.AddDays(-5),
                    InStock = false
                }
            };
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}