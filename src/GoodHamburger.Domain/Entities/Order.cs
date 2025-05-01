namespace GoodHamburger.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int? SandwichId { get; set; }
    public int? FriesId { get; set; }
    public int? SoftDrinkId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
