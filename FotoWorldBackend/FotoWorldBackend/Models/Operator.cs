using System;
using System.Collections.Generic;

namespace FotoWorldBackend.Models;

public partial class Operator
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool IsCompany { get; set; }

    public string Availability { get; set; } = null!;

    public string LocationCity { get; set; } = null!;

    public int OperatingRadius { get; set; }

    public int Services { get; set; }

    public virtual ICollection<Offer> Offers { get; } = new List<Offer>();

    public virtual ICollection<OperatorRating> OperatorRatings { get; } = new List<OperatorRating>();

    public virtual OperatorService ServicesNavigation { get; set; } = null!;
}
