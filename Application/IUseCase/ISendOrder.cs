using Domain.Dtos;
using Domain.Entities;

namespace Application.ICaseUse;

public interface ISendOrder
{
    Task<int> EnviarProducto(int id, SendProductDto sendProductDto);
    Task<bool> ConfirmarEnvio(int id);
    Task<GetSendOrderDomain> verEnvio(int id);
}