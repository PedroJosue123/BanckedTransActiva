using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class GetPreparationMapper
{
    public static GetPrepationDomain ToDomain(Pedido entity)
    {
        // Extraer los estados
        

        bool estadoEnviado = (bool)entity.IdPedidosProductosNavigation.IdPreparacionNavigation.IdEnvioNavigation.Estado;
        bool estadoPreparado = (bool)entity.IdPedidosProductosNavigation.IdPreparacionNavigation.Estado;

        // Lógica del estado
        string estadoString;

       if (estadoEnviado && estadoPreparado)
        {
            estadoString = "Preparado y enviado";
        }
        else if (estadoPreparado && !estadoEnviado)
        {
            
            estadoString = "Pedido solo esta preparado pero no enviado";
        }
        else
        {
            estadoString = "No se ah preparado";
        }

        return new GetPrepationDomain(
            estadoString,
            entity.IdPedidosProductosNavigation.FechaSolicitada,
            entity.IdPedidosProductosNavigation.IdPagoNavigation.FechaPago, 
            entity.IdCompradorNavigation.Userprofile.Name,
            entity.IdPedidosProductosNavigation.FechaLlegadaAcordada,
            entity.IdPedidosProductosNavigation.Producto,
            entity.IdPedidosProductosNavigation.Cantidad,
            entity.IdPedidosProductosNavigation.Descripcion,
            entity.IdPedidosProductosNavigation.DireccionEntrega,
            entity.IdPedidosProductosNavigation.IdPagoNavigation.Monto,
            entity.IdPedidosProductos
           
        );
    }
}