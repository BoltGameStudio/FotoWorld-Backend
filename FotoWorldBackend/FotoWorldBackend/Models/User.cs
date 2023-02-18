using System;
using System.Collections.Generic;

namespace FotoWorldBackend.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool IsActice { get; set; }

    public virtual ICollection<FollowedOffer> FollowedOffers { get; } = new List<FollowedOffer>();

    public virtual ICollection<OperatorRating> OperatorRatings { get; } = new List<OperatorRating>();
}
