/*using System.Security.Claims;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace TransActiva.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PurchaserController : ControllerBase
{
    private readonly IOrderService _orderService;

    public PurchaserController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize(Roles = "Comprador")]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _orderService.CreateOrderAsync(dto, userId);
        return result ? Ok("Pedido creado correctamente.") : BadRequest("No se pudo crear el pedido.");
    }

    [HttpGet]
    [Authorize(Roles = "Purchaser,Vendor")]
    public async Task<IActionResult> Get()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var orders = await _orderService.GetOrdersByUserAsync(userId);
        return Ok(orders);
    }

}*/