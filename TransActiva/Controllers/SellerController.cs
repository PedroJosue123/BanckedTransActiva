using System.Security.Claims;
using Application.ICaseUse;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace TransActiva.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SellerController (IOrderRequests orderRequests,IOrder order): ControllerBase
{
   
    
    [Authorize(Roles = "Vendedor")]
    [HttpGet("{id}")]
   
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
    
    [HttpPut("{id}")]
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
    
    [HttpPost("Preparar Producto")]
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
    
}