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
            var registro = await order.RegisterOrder(registerOrderRequestDto);
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
    [HttpGet("Mostarlospedidos{id}")]
    public async Task<IActionResult> MostrarOrder(int id)
    {
        try
        {
            var registro = await order.MostrarOrder(id);
            return Ok (new {registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
}