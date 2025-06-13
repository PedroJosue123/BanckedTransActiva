using Domain.Dtos;

namespace Application.ICaseUse;

public interface IRegisterUser
{
    Task<bool> Execute(RegisterRequestDto request);
}
