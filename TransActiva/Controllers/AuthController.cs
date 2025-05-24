using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TransActiva.Dtos;
using TransActiva.Interface;

namespace TransActiva.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var success = await _authService.RegisterAsync(request);
        return success ? Ok(new { message = "Usuario registrado correctamente." })
            : BadRequest(new { error = "El correo electrónico ya está registrado." });
    }

   
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var token = await _authService.LoginAsync(request);

        return token == null
            ? Unauthorized(new { error = "Credenciales inválidas." })
            : Ok(new { token });
    }
}