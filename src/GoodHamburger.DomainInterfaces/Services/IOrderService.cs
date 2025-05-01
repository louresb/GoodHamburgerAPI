using GoodHamburger.Domain.DTOs;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.DomainInterfaces.Services;

public interface IOrderService
{
    Order Create(OrderRequest request);
    List<Order> GetAll();
    Order Update(int id, OrderRequest request);
    void Delete(int id);
}
