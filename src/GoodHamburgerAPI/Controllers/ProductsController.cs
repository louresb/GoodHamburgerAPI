using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.DomainInterfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        var products = _productService.GetAll();
        return Ok(products);
    }

    [HttpGet("sandwiches")]
    public ActionResult<IEnumerable<Product>> GetSandwiches()
    {
        var sandwiches = _productService.GetByType(ProductType.Sandwich);
        return Ok(sandwiches);
    }

    [HttpGet("extras")]
    public ActionResult<IEnumerable<Product>> GetExtras()
    {
        var extras = _productService.GetByType(ProductType.Extra);
        return Ok(extras);
    }
}
