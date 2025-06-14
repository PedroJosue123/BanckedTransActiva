

using Domain.Dtos;

namespace Application.ICaseUse;

public interface ILoginUser
{
    Task<string?> Execute(LoginRequestDto request);
    
}