using System;
using System.Collections.Generic;

namespace TransActiva.Models;

public partial class order
{
    public int OrderId { get; set; }

    public string Product { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Status { get; set; }

    public string? Supplier { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual user User { get; set; } = null!;
}
