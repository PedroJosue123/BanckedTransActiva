namespace Domain.Entities;

public class GetPrepationDomain
{
    public string? EstadoString { get; set; }
    public DateTime? FechaSolicitada { get; set; }
    public DateTime? FechaPago { get; set; }
    public string? NameComprador { get; }
    public DateTime FechaLlegadaAcordada { get; set; }

    public string Producto { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Descripcion { get; set; } = null!;
    
    

    public string DireccionEntrega { get; set; } = null!;
    
    public decimal? Monto { get; set; }
    
    public int IdPedidosProductos { get; private set; }

  
    
    
    
    public GetPrepationDomain(
        string? estadoString,
        DateTime? fechaSolicitada,
        DateTime? fechaPago,
        string? namecomprador,
        DateTime fechaLlegadaAcordada,
        string producto,
        int cantidad,
        string descripcion,
        string direccionEntrega,
        decimal? monto,
        int idPedidosProductos
        )
    {
       
        if (string.IsNullOrWhiteSpace(producto)) throw new ArgumentException("Producto es obligatorio.");
        if (string.IsNullOrWhiteSpace(descripcion)) throw new ArgumentException("Descripcion es obligatoria.");
        if (string.IsNullOrWhiteSpace(direccionEntrega)) throw new ArgumentException("DireccionEntrega es obligatoria.");
        if (cantidad <= 0) throw new ArgumentException("Cantidad debe ser mayor a cero.");
        if (monto == null || monto <= 0) throw new ArgumentException("Monto debe ser mayor a cero.");
        if (fechaLlegadaAcordada == default) throw new ArgumentException("FechaLlegadaAcordada es obligatoria.");

        EstadoString = estadoString;
        FechaSolicitada = fechaSolicitada;
        FechaPago = fechaPago;
        Producto = producto;
        Cantidad = cantidad;
        Descripcion = descripcion;
        NameComprador = namecomprador;
        DireccionEntrega = direccionEntrega;
        Monto = monto;
        FechaLlegadaAcordada = fechaLlegadaAcordada;
        IdPedidosProductos = idPedidosProductos;

    }
}