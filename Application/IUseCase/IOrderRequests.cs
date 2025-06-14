using Domain.Dtos;
using Domain.Entities;

namespace Application.ICaseUse;

public interface IOrderRequests
{

    Task<List<OrderGetRequestDomain>> GetSolicitud(int id);
    
    Task<bool> AceptarSolicitud(int id);
}