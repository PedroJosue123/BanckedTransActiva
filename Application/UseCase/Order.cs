using Application.ICaseUse;
using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.CaseUse;

public class Order  (IUnitOfWork unitOfWork)  : IOrder
{
    public async Task<int> RegisterOrder(RegisterOrderRequestDto requestDto)
    {
        
        var usuariosEncontrados = await unitOfWork.Repository<User>()
            .GetAll()
            .CountAsync(u => u.UserId == requestDto.IdComprador || u.UserId == requestDto.Idproveedor);

        if (usuariosEncontrados < 2)
            throw new Exception("Uno o ambos usuarios no existen");

       
        var order = new OrderDomain(requestDto.Idproveedor, requestDto.IdComprador, 0, false);

        var orderef = OrderMapper.ToEntity(order);

        var detalles_producto = new ProductOrdenDomain( 0, requestDto.Producto, requestDto.Cantidad , requestDto.Descripcion, 
            requestDto.DireccionEntrega, requestDto.FechaLlegadaAcordada, requestDto.NombreTransaccion , 0);

        var detalleef = ProductMapper.ToEntity(detalles_producto);
        detalleef.FechaSolicitada =  DateTime.Now;
        var pago = new PaymentsDomain(0, null,  false, requestDto.Monto  );
        var pagoef = PaymentMapper.ToEntity(pago);
        
        try
        {
            await unitOfWork.BeginTransactionAsync();

            // Guardamos primero el usuario
            await unitOfWork.Repository<Pago>().AddAsync(pagoef);
            await unitOfWork.SaveChange();
            
            detalleef.IdPago = pagoef.IdPago;
            
            await unitOfWork.Repository<Pedidosproducto>().AddAsync(detalleef);
            await unitOfWork.SaveChange();

            // Asociamos el ID generado al perfil
            orderef.IdPedidosProductos = detalleef.IdPedidosProductos;

            // Guardamos el perfil
            await unitOfWork.Repository<Pedido>().AddAsync(orderef);
            await unitOfWork.SaveChange();
            
            
            await unitOfWork.CommitTransactionAsync();
            return orderef.IdPedido;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new Exception("Datos Incompletos");

        }
        
    }

    public async Task<List<GetPrepationDomain>> GetPreparationOrder(int id)
    {
        var pedido = await unitOfWork.Repository<Pedido>()
            .GetAll()
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPreparacionNavigation)
            .ThenInclude(prep => prep.IdEnvioNavigation) // Muy importante
            .Include(p => p.IdCompradorNavigation)
            .ThenInclude(c => c.Userprofile)
            .Where(u => u.IdProveedor == id)
            .ToListAsync();

        
        if (!pedido.Any()) throw new Exception("No hay ni mierda");
        var pedidosDominio = pedido.Select(p => GetPreparationMapper.ToDomain(p)).ToList();

        return pedidosDominio;
    }

    public async Task<bool> PreparetedOrder(int id, PreparationOrderDto preparationOrderDto)
    {

        var preparacion = new PreparationOrderDomain(0 ,preparationOrderDto.ComoEnvia ,preparationOrderDto.Detalles);

        var preparacionEntity = PreparationOrderMapper.ToEntity(preparacion);
        await unitOfWork.Repository<Preparacion>().AddAsync(preparacionEntity);
        
        await unitOfWork.SaveChange();
    
        var pediproducto =  await unitOfWork.Repository<Pedidosproducto>().GetByIdAsync(id);
        
        

        pediproducto.IdPreparacion = preparacionEntity.IdPreparacion;

        await unitOfWork.SaveChange();

        return true;

    }

    public async Task<bool> VerSiOrderAceptado(int id)
    {
        var pedido = await unitOfWork.Repository<Pedido>().GetByIdAsync(id);
        
        if ((bool)!pedido.Estado) throw new Exception("No fue pagado");

        return true;

    }

    public async Task<List<GetOrderDomain>> MostrarOrder(int id)
    {


        var pedido = await unitOfWork.Repository<Pedido>()
            .GetAll().Include(p => p.IdPedidosProductosNavigation) // Incluye productos del pedido
            .ThenInclude(pp => pp.IdPagoNavigation) // Incluye pago dentro del producto
            .Include(p => p.IdProveedorNavigation)
            .ThenInclude(pp => pp.Userprofile) // Cliente// Proveedor
            .Where(u => u.IdComprador == id).ToListAsync();
            
        if (!pedido.Any()) throw new Exception("No hay ni mierda");
        var pedidosDominio = pedido.Select(p => GetOrderMapper.ToDomain(p)).ToList();

        return pedidosDominio;


    }
}