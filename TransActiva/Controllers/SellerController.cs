using System.Security.Claims;
using Application.ICaseUse;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace TransActiva.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SellerController (IOrderRequests orderRequests,IOrder order, ISendOrder sendOrder): ControllerBase
{
   
    
    [Authorize(Roles = "Vendedor")]
    [HttpGet("ObtenerSolicitudes/{id}")]
   
    public async Task<IActionResult> GetPedidoById(int id)
    {
        try
        {
            var registro = await orderRequests.GetSolicitud(id);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPut("ActivarSolicitud/{id}")]
    public async Task<IActionResult> ActivarSolicitud(int id)
    {
        try
        {
            var registro = await orderRequests.AceptarSolicitud(id);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpGet("Confirmarsipago/{id}")]
    public async Task<IActionResult> Versiapagado(int id)
    {
        try
        {
            var registro = await orderRequests.VerSiPago(id);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpGet("VerlistaPreparacion{id}")]
    public async Task<IActionResult> listadepreparacion(int id)
    {
        try
        {
            var registro = await order.GetPreparationOrder(id);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPost("PrepararProducto")]
    public async Task<IActionResult> PrepararProducto(int id, [FromBody] PreparationOrderDto preparationOrderDto )
    {
        try
        {
            var registro = await order.PreparetedOrder(id, preparationOrderDto );
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPost("EnviarProducto")]
    public async Task<IActionResult> EnviarProducto(int id, [FromBody] SendProductDto sendProductDto )
    {
        try
        {
            var registro = await sendOrder.EnviarProducto(id, sendProductDto);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPost("ConfirmarEnvio")]
    public async Task<IActionResult> ConfirmarEnvio(int id )
    {
        try
        {
            var registro = await sendOrder.ConfirmarEnvio(id);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}