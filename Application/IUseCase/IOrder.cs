using Domain.Dtos;
using Domain.Entities;
using Infraestructure.Models;

namespace Application.ICaseUse;

public interface IOrder
{
    Task<int> RegisterOrder(RegisterOrderRequestDto requestDto);
    Task<bool> PreparetedOrder(int id, PreparationOrderDto preparationOrderDto);
}