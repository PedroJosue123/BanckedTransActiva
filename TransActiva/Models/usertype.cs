using System;
using System.Collections.Generic;

namespace TransActiva.Models;

public partial class usertype
{
    public int UserTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<user> users { get; set; } = new List<user>();
}
