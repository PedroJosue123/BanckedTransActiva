using Application.CaseUse;
using Application.ICaseUse;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TransActiva.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuyerController (IOrder order) : ControllerBase
{
   
    [Authorize(Roles = "Comprador")]
    [HttpPost("Comprador")]
    public async Task<IActionResult> Login([FromBody] RegisterOrderRequestDto registerOrderRequestDto)
    {
        try
        {
            var registro = await order.RegisterOrder(registerOrderRequestDto);
            return Ok (new { registered = registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
}