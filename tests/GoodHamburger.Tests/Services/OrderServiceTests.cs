using Moq;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.DTOs;
using GoodHamburger.Domain.Enums;
using GoodHamburger.DomainInterfaces.Services;
using GoodHamburger.Infra.Services;
using Microsoft.Extensions.Caching.Memory;

namespace GoodHamburger.Tests.Services;

public class OrderServiceTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly IMemoryCache _memoryCache;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _mockProductService = new Mock<IProductService>();
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _orderService = new OrderService(_memoryCache, _mockProductService.Object);

        _mockProductService.Setup(p => p.GetAll()).Returns(new List<Product>
        {
            new Product { Id = 1, Name = "X Burger", Price = 5.00m, Type = ProductType.Sandwich },
            new Product { Id = 4, Name = "Fries", Price = 2.00m, Type = ProductType.Extra },
            new Product { Id = 5, Name = "Soft Drink", Price = 2.50m, Type = ProductType.Extra },
        });
    }

    [Fact]
    public void Should_Apply_20Percent_Discount_When_Sandwich_Fries_And_Soda_Selected()
    {
        var request = new OrderRequest
        {
            SandwichId = 1,
            FriesId = 4,
            SoftDrinkId = 5
        };

        var order = _orderService.Create(request);

        // 5.00 + 2.00 + 2.50 = 9.50 → 20% = 7.60
        Assert.Equal(7.60m, order.TotalPrice);
    }

    [Fact]
    public void Should_Throw_When_Same_Product_Used_Twice()
    {
        var request = new OrderRequest
        {
            SandwichId = 1,
            FriesId = 1,
            SoftDrinkId = 5
        };

        var ex = Assert.Throws<ArgumentException>(() => _orderService.Create(request));
        Assert.Equal("Each item in the order must be unique.", ex.Message);
    }

    [Fact]
    public void Should_Apply_15Percent_Discount_When_Sandwich_And_SoftDrink_Selected()
    {
        var request = new OrderRequest
        {
            SandwichId = 1,
            SoftDrinkId = 5
        };

        var order = _orderService.Create(request);

        // 5.00 + 2.50 = 7.50 → 15% = 6.375
        Assert.Equal(6.375m, order.TotalPrice);
    }

    [Fact]
    public void Should_Apply_10Percent_Discount_When_Sandwich_And_Fries_Selected()
    {
        var request = new OrderRequest
        {
            SandwichId = 1,
            FriesId = 4
        };

        var order = _orderService.Create(request);

        // 5.00 + 2.00 = 7.00 → 10% = 6.30
        Assert.Equal(6.30m, order.TotalPrice);
    }

    [Fact]
    public void Should_Throw_When_Order_Has_No_Items()
    {
        var request = new OrderRequest();

        var ex = Assert.Throws<ArgumentException>(() => _orderService.Create(request));
        Assert.Equal("The order must contain at least one item.", ex.Message);
    }

    [Fact]
    public void Should_Throw_When_Invalid_SandwichId_Provided()
    {
        var request = new OrderRequest
        {
            SandwichId = 99
        };

        var ex = Assert.Throws<ArgumentException>(() => _orderService.Create(request));
        Assert.Equal("Invalid sandwich ID.", ex.Message);
    }
}
