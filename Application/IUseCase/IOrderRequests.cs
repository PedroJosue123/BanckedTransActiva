using Domain.Dtos;
using Domain.Entities;

namespace Application.ICaseUse;

public interface IOrderRequests
{
    Task<List<PedidoDto>> GetSolicitudes();
    Task<OrderRequestDomain> GetAceptarSolicitud(int id);
    
    Task<bool> AceptarSolicitud(int id);
}