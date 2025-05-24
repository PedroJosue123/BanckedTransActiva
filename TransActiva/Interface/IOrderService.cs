using TransActiva.Dtos;
using TransActiva.Models;

namespace TransActiva.Interface;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(CreateOrderDto dto, int userId);
    Task<IEnumerable<order>> GetOrdersByUserAsync(int userId);
}
