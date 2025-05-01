using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.DomainInterfaces.Services;

public interface IProductService
{
    List<Product> GetAll();
    List<Product> GetByType(ProductType type);
}
