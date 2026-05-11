using fourth_term_software_labs.Data;
using fourth_term_software_labs.Models;
using Microsoft.EntityFrameworkCore;

namespace fourth_term_software_labs.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public EfProductRepository(AppDbContext context)
        {
            _context = context;
        }
        // ========== СУЩЕСТВУЮЩИЕ МЕТОДЫ ==========
        public IEnumerable<Product> GetAll() => _context.Products.ToList();
        public Product? GetById(int id) => _context.Products.Find(id);
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Product> GetByCategory(string category) =>
        _context.Products.Where(p => p.Category == category).ToList();
        public IEnumerable<Product> GetInStock() =>
        _context.Products.Where(p => p.InStock).ToList();
        // ========== НОВЫЕ LINQ-МЕТОДЫ ==========
        /// <summary>
        /// Фильтрация товаров по диапазону цен
        /// </summary>
        public IEnumerable<Product> GetProductsByPriceRange(decimal minPrice,
        decimal maxPrice) =>
        _context.Products
        .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
        .OrderBy(p => p.Price)
        .ToList();
        /// <summary>
        /// Получение топ N самых дорогих товаров
        /// </summary>
        public IEnumerable<Product> GetTopExpensiveProducts(int count) =>
        _context.Products
        .OrderByDescending(p => p.Price)
        .Take(count)
        .ToList();
        /// <summary>
        /// Поиск товаров по названию, описанию и категории
        /// </summary>
        public IEnumerable<Product> SearchProducts(string searchTerm) =>
        _context.Products
        .Where(p => p.Name.Contains(searchTerm) ||
        p.Description.Contains(searchTerm) ||
        p.Category.Contains(searchTerm))
        .OrderBy(p => p.Name)
        .ToList();
        /// <summary>
        /// Средняя цена всех товаров
        /// </summary>
        public decimal GetAveragePrice() =>
        _context.Products.Average(p => p.Price);
        /// <summary>
        /// Общее количество товаров
        /// </summary>
        public int GetTotalCount() =>
        _context.Products.Count();
        /// <summary>
        /// Диапазон цен (минимальная и максимальная)
        /// </summary>
        public (decimal MinPrice, decimal MaxPrice) GetPriceRange()
        {
            return (
            MinPrice: _context.Products.Min(p => p.Price),
            MaxPrice: _context.Products.Max(p => p.Price)
            );
        }
        /// <summary>
        /// Проверка наличия товаров в указанной категории
        /// </summary>
        public bool AnyInCategory(string category) =>
        _context.Products.Any(p => p.Category == category);
        /// <summary>
        /// Группировка товаров по категориям
        /// </summary>
        public IEnumerable<IGrouping<string, Product>>
        GetProductsGroupedByCategory() =>
        _context.Products
        .GroupBy(p => p.Category)
        .OrderBy(g => g.Key)
        .ToList();
        /// <summary>
        /// Пагинация: получение товаров для указанной страницы
        /// </summary>
        public IEnumerable<Product> GetProductsWithPagination(int page, int
        pageSize) =>
        _context.Products
        .OrderBy(p => p.Id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();
        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int GetTotalPages(int pageSize)
        {
            var totalCount = GetTotalCount();
            return (int)Math.Ceiling(totalCount / (double)pageSize);
        }
        // ========== АСИНХРОННЫЕ МЕТОДЫ ==========
        public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _context.Products.ToListAsync();
        public async Task<Product?> GetByIdAsync(int id) =>
        await _context.Products.FindAsync(id);
        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal
        minPrice, decimal maxPrice) =>
        await _context.Products
        .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
        .OrderBy(p => p.Price)
        .ToListAsync();
        public async Task<decimal> GetAveragePriceAsync() =>
        await _context.Products.AverageAsync(p => p.Price);
        public async Task<int> GetTotalCountAsync() =>
        await _context.Products.CountAsync();
        public async Task<IEnumerable<IGrouping<string, Product>>>
        GetProductsGroupedByCategoryAsync() =>
        await _context.Products
        .GroupBy(p => p.Category)
        .OrderBy(g => g.Key)
        .ToListAsync();
    }
}