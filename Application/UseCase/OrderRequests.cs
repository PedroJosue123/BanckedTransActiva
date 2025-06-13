using Application.ICaseUse;
using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.CaseUse;

public class OrderRequests (IUnitOfWork unitOfWork) : IOrderRequests
{
    public async Task<List<PedidoDto>> GetSolicitudes()
    {
        var solicitudes = await unitOfWork.Repository<Pedido>()
            .GetAll()
            .Where(u => u.Estado == false)
            .ToListAsync();

        if (!solicitudes.Any())
            throw new Exception("No hay solicitudes en espera");

        var resultado = solicitudes.Select(p => new PedidoDto
        {
            IdPedido = p.IdPedido,
            IdProveedor = p.IdProveedor,
            IdComprador = p.IdComprador,
            IdPedidosProductos = p.IdPedidosProductos,
            Estado = p.Estado
        }).ToList();

        return resultado;
    }
    
    public async Task<OrderRequestDomain> GetAceptarSolicitud(int id)
    {
        var pedido = await unitOfWork.Repository<Pedido>()
            .GetAll()
            .Include(p => p.IdPedidosProductosNavigation)                   // Incluye productos del pedido
            .ThenInclude(pp => pp.IdPagoNavigation)                    // Incluye pago dentro del producto
            .Include(p => p.IdCompradorNavigation)
            .ThenInclude(pp => pp.Userprofile)                            // Cliente// Proveedor
            .FirstOrDefaultAsync(p => p.IdPedido == id);


    
        if (pedido.IdPedidosProductosNavigation.IdPagoNavigation.Monto== null)
            throw new Exception("Falta el pago o no existe el pedido");


        var pedidoDomain = new OrderRequestDomain(pedido.IdCompradorNavigation.Userprofile.Name, pedido.IdPedidosProductosNavigation.Producto,
            pedido.IdPedidosProductosNavigation.Cantidad ,pedido.IdPedidosProductosNavigation.Descripcion ,pedido.IdPedidosProductosNavigation.DireccionEntrega,
            pedido.IdPedidosProductosNavigation.FechaLlegadaAcordada,pedido.IdPedidosProductosNavigation.IdPagoNavigation.Monto);
        

        return pedidoDomain;
    }
    
    public async Task<bool> AceptarSolicitud(int id)
    {
        var pedido = await unitOfWork.Repository<Pedido>()
            .GetAll() 
            .FirstOrDefaultAsync(p => p.IdPedido == id);

        var pedidoDomain = OrderMapper.ToDomain(pedido);

        pedidoDomain.Estado = true;

        var pedidoEntity = OrderMapper.ToEntity(pedidoDomain);

        unitOfWork.Repository<Pedido>().UpdateAsync(pedidoEntity.IdPedido, pedidoEntity);
        
        unitOfWork.SaveChange();

        

        return true;
    }
}