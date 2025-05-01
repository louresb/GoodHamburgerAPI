using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.DomainInterfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace GoodHamburger.Infra.Services;

public class ProductService : IProductService
{
    private readonly IMemoryCache _memoryCache;
    private const string ProductsKey = "Products";

    public ProductService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        InitializeProducts();
    }

    private void InitializeProducts()
    {
        if (!_memoryCache.TryGetValue(ProductsKey, out List<Product>? _))
        {
            var products = new List<Product>
            {
                new() { Id = 1, Name = "X Burger", Price = 5.00m, Type = ProductType.Sandwich },
                new() { Id = 2, Name = "X Egg", Price = 4.50m, Type = ProductType.Sandwich },
                new() { Id = 3, Name = "X Bacon", Price = 7.00m, Type = ProductType.Sandwich },
                new() { Id = 4, Name = "Fries", Price = 2.00m, Type = ProductType.Extra },
                new() { Id = 5, Name = "Soft Drink", Price = 2.50m, Type = ProductType.Extra }
            };

            _memoryCache.Set(ProductsKey, products);
        }
    }

    public List<Product> GetAll()
    {
        return _memoryCache.Get<List<Product>>(ProductsKey) ?? new();
    }

    public List<Product> GetByType(ProductType type)
    {
        var products = GetAll();
        return products.Where(p => p.Type == type).ToList();
    }
}
