using GoodHamburger.Domain.DTOs;
using GoodHamburger.Domain.Entities;
using GoodHamburger.DomainInterfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public ActionResult<Order> CreateOrder([FromBody] OrderRequest request)
    {
        try
        {
            var createdOrder = _orderService.Create(request);
            return CreatedAtAction(nameof(GetAll), new { id = createdOrder.Id }, createdOrder);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAll()
    {
        var orders = _orderService.GetAll();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = _orderService.GetAll().FirstOrDefault(o => o.Id == id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}")]
    public ActionResult<Order> UpdateOrder(int id, [FromBody] OrderRequest request)
    {
        try
        {
            var updatedOrder = _orderService.Update(id, request);
            return Ok(updatedOrder);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { error = "Order not found." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        try
        {
            _orderService.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { error = "Order not found." });
        }
    }
}
