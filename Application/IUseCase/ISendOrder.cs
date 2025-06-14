using Domain.Dtos;

namespace Application.ICaseUse;

public interface ISendOrder
{
    Task<bool> EnviarProducto(int id, SendProductDto sendProductDto);
    Task<bool> ConfirmarEnvio(int id);
}