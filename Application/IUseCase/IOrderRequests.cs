using Domain.Dtos;
using Domain.Entities;

namespace Application.ICaseUse;

public interface IOrderRequests
{

    Task<List<OrderGetRequestDomain>> GetSolicitud(int id);
    
    Task<int> AceptarSolicitud(int id);

    Task<int> VerSiPago(int id);
}