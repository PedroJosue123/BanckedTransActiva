using System.Security.Claims;
using Application.CaseUse;
using Application.ICaseUse;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TransActiva.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuyerController (IOrder order, IPaymentOrder paymentOrder) : ControllerBase
{
   
    [Authorize(Roles = "Comprador")]
    [HttpPost("Comprador")]
    public async Task<IActionResult> Login([FromBody] RegisterOrderRequestDto registerOrderRequestDto)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");

            int userId = int.Parse(userIdClaim.Value);
            var registro = await order.RegisterOrder(registerOrderRequestDto , userId);
            return Ok (new { Idpedido = registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpGet("VersOrdenAceptada{id}")]
    public async Task<IActionResult> VersiPago(int id)
    {
        try
        {
            var registro = await order.VerSiOrderAceptado(id);
            return Ok (new { Idpedido = registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpPost("VistaPagar")]
    public async Task<IActionResult> GetPayment(int id)
    {
        try
        {
            var registro = await paymentOrder.GeyDataPayment(id);
            return Ok (new { registered = registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpPost("Pagar")]
    public async Task<IActionResult> Payment(int id, [FromBody] PaymentCartDto paymentCartDto)
    {
        try
        {
            var registro = await paymentOrder.Payment(id, paymentCartDto);
            return Ok (new {registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpGet("Mostarlospedidos")]
    public async Task<IActionResult> MostrarOrder()
    
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");

            int userId = int.Parse(userIdClaim.Value);

            var registro = await order.MostrarOrder(userId);
            return Ok (new {registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
}