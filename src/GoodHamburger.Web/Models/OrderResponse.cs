namespace GoodHamburger.Web.Models;

public class OrderResponse
{
    public int Id { get; set; }
    public int? SandwichId { get; set; }
    public int? FriesId { get; set; }
    public int? SoftDrinkId { get; set; }
    public decimal TotalPrice { get; set; }
}
