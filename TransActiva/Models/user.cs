using System;
using System.Collections.Generic;

namespace TransActiva.Models;

public partial class user
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int UserTypeId { get; set; }

    public int? FailedLoginAttempts { get; set; }

    public DateTime? LockoutUntil { get; set; }

    public virtual usertype UserType { get; set; } = null!;

    public virtual ICollection<order> orders { get; set; } = new List<order>();

    public virtual userprofile? userprofile { get; set; }
}
