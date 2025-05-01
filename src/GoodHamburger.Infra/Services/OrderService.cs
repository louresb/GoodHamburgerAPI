using GoodHamburger.Domain.DTOs;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.DomainInterfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace GoodHamburger.Infra.Services;

public class OrderService : IOrderService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IProductService _productService;
    private const string OrdersKey = "Orders";

    public OrderService(IMemoryCache memoryCache, IProductService productService)
    {
        _memoryCache = memoryCache;
        _productService = productService;
    }

    public Order Create(OrderRequest request)
    {
        if (request.SandwichId is null && request.FriesId is null && request.SoftDrinkId is null)
            throw new ArgumentException("The order must contain at least one item.");

        var selectedIds = new[] { request.SandwichId, request.FriesId, request.SoftDrinkId }
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .ToList();

        if (selectedIds.Count != selectedIds.Distinct().Count())
            throw new ArgumentException("Each item in the order must be unique.");

        var products = _productService.GetAll();
        decimal total = 0;

        var sandwich = products.FirstOrDefault(p => p.Id == request.SandwichId && p.Type == ProductType.Sandwich);
        var fries = products.FirstOrDefault(p => p.Id == request.FriesId && p.Name == "Fries");
        var softDrink = products.FirstOrDefault(p => p.Id == request.SoftDrinkId && p.Name == "Soft Drink");

        if (request.SandwichId != null && sandwich == null)
            throw new ArgumentException("Invalid sandwich ID.");
        if (request.FriesId != null && fries == null)
            throw new ArgumentException("Invalid fries ID.");
        if (request.SoftDrinkId != null && softDrink == null)
            throw new ArgumentException("Invalid soft drink ID.");

        if (sandwich != null) total += sandwich.Price;
        if (fries != null) total += fries.Price;
        if (softDrink != null) total += softDrink.Price;

        var discount = 0.0m;

        if (sandwich != null && fries != null && softDrink != null)
            discount = 0.20m;
        else if (sandwich != null && softDrink != null)
            discount = 0.15m;
        else if (sandwich != null && fries != null)
            discount = 0.10m;

        var finalPrice = total * (1 - discount);

        var newOrder = new Order
        {
            Id = GetNextId(),
            SandwichId = sandwich?.Id,
            FriesId = fries?.Id,
            SoftDrinkId = softDrink?.Id,
            TotalPrice = finalPrice
        };

        var orders = GetAll();
        orders.Add(newOrder);
        _memoryCache.Set(OrdersKey, orders);

        return newOrder;
    }

    public List<Order> GetAll()
    {
        if (!_memoryCache.TryGetValue(OrdersKey, out List<Order>? orders))
        {
            orders = new List<Order>();
            _memoryCache.Set(OrdersKey, orders);
        }

        return orders!;
    }

    private int GetNextId()
    {
        var orders = GetAll();
        return orders.Any() ? orders.Max(o => o.Id) + 1 : 1;
    }

    public Order Update(int id, OrderRequest request)
    {
        var orders = GetAll();
        var existingOrder = orders.FirstOrDefault(o => o.Id == id);

        if (existingOrder == null)
            throw new KeyNotFoundException("Order not found.");

        var products = _productService.GetAll();
        decimal total = 0;

        var sandwich = products.FirstOrDefault(p => p.Id == request.SandwichId && p.Type == ProductType.Sandwich);
        var fries = products.FirstOrDefault(p => p.Id == request.FriesId && p.Name == "Fries");
        var softDrink = products.FirstOrDefault(p => p.Id == request.SoftDrinkId && p.Name == "Soft Drink");

        if (request.SandwichId != null && sandwich == null)
            throw new ArgumentException("Invalid sandwich ID.");
        if (request.FriesId != null && fries == null)
            throw new ArgumentException("Invalid fries ID.");
        if (request.SoftDrinkId != null && softDrink == null)
            throw new ArgumentException("Invalid soft drink ID.");

        if (sandwich != null) total += sandwich.Price;
        if (fries != null) total += fries.Price;
        if (softDrink != null) total += softDrink.Price;

        var discount = 0.0m;
        if (sandwich != null && fries != null && softDrink != null)
            discount = 0.20m;
        else if (sandwich != null && softDrink != null)
            discount = 0.15m;
        else if (sandwich != null && fries != null)
            discount = 0.10m;

        var finalPrice = total * (1 - discount);

        existingOrder.SandwichId = sandwich?.Id;
        existingOrder.FriesId = fries?.Id;
        existingOrder.SoftDrinkId = softDrink?.Id;
        existingOrder.TotalPrice = finalPrice;

        _memoryCache.Set(OrdersKey, orders);
        return existingOrder;
    }

    public void Delete(int id)
    {
        var orders = GetAll();
        var existingOrder = orders.FirstOrDefault(o => o.Id == id);

        if (existingOrder == null)
            throw new KeyNotFoundException("Order not found.");

        orders.Remove(existingOrder);
        _memoryCache.Set(OrdersKey, orders);
    }
}
