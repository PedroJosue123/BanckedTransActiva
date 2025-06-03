using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public static class UserProfileMapper
{
    public static userprofile ToEntity(UserProfile domain) => new userprofile
    {
        UserId = domain.UserId,
        Name = domain.Name,
        Ruc = domain.Ruc,
        ManagerName = domain.ManagerName,
        ManagerDni = domain.ManagerDni,
        ManagerEmail = domain.ManagerEmail,
        Phone = domain.Phone,
        Address = domain.Address
    };
}