using Domain.Dtos;
using Domain.Entities;
using Infraestructure.Models;

namespace Application.ICaseUse;

public interface IOrder
{
    Task<bool> RegisterOrder(RegisterOrderRequestDto requestDto);
    
}