using Application.ICaseUse;
using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using Infraestructure.Service;
using Microsoft.EntityFrameworkCore;

namespace Application.CaseUse;

public class PaymentOrder(IUnitOfWork unitOfWork, IPaymentServer paymentServer) : IPaymentOrder
{
    public async Task<PaymentGetRequestDomain> GeyDataPayment(int id)
    {
        var Pedido = await unitOfWork.Repository<Pedido>().GetAll()
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .FirstOrDefaultAsync(u => u.IdPedido == id && u.Estado == true);
        if (Pedido == null) throw new Exception("No hay ni mierda");
        var pedidodomain = PaymentGetOrderMapper.ToDomain(Pedido);

      
        
        return pedidodomain;


    }
    
    public async Task<Boolean> Payment(int id, PaymentCartDto card)
    {
        var Pedido = await unitOfWork.Repository<Pedido>().GetAll()
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .FirstOrDefaultAsync(u => u.IdPedido == id && u.Estado == true);
        
        
        if (Pedido == null) throw new Exception("No hay ni mierda");

        var validatecard = paymentServer.paymentt(card.NroTarjeta, card.FechaNacimiento, card.cvv);

        if (!validatecard) throw new Exception("No se puede pagar");
        
        Pedido.IdPedidosProductosNavigation.IdPagoNavigation.Estado = true;
        Pedido.IdPedidosProductosNavigation.IdPagoNavigation.FechaPago =  DateTime.Now;
        await unitOfWork.SaveChange();
        return true;

    }
    
}