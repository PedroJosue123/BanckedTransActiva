using Domain.Dtos;
using Domain.Entities;
using Infraestructure.Models;

namespace Application.ICaseUse;

public interface IOrder
{
    Task<int> RegisterOrder(RegisterOrderRequestDto requestDto);
    Task<List<GetPrepationDomain>> GetPreparationOrder(int id);
    Task<bool> PreparetedOrder(int id, PreparationOrderDto preparationOrderDto);
    Task<bool> VerSiOrderAceptado(int id);
    Task<List<GetOrderDomain>> MostrarOrder(int id);
}