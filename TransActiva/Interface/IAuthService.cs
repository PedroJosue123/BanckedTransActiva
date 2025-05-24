using TransActiva.Dtos;

namespace TransActiva.Interface;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginRequestDto request);
    Task<bool> RegisterAsync(RegisterRequestDto request);
}
