namespace GoodHamburger.Domain.DTOs;

public class OrderRequest
{
    public int? SandwichId { get; set; }
    public int? FriesId { get; set; }
    public int? SoftDrinkId { get; set; }
}
