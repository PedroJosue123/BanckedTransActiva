using Domain.Entities;

using Infraestructure.Models;

namespace Application.Mappers;

public class UserMapper
{
    public static User ToDomain(user entity) => new User(
        entity.UserId, entity.Email, entity.Password, entity.CreatedAt,
        entity.UserTypeId, entity.FailedLoginAttempts ?? 0, entity.LockoutUntil);

    public static user ToEntity(User domain) => new user
    {
        UserId = domain.Id,
        Email = domain.Email,
        Password = domain.PasswordHash,
        CreatedAt = domain.CreatedAt,
        UserTypeId = domain.UserTypeId,
        FailedLoginAttempts = domain.FailedLoginAttempts,
        LockoutUntil = domain.LockoutUntil
    };
}

