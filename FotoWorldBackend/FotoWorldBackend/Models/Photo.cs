using System;
using System.Collections.Generic;

namespace FotoWorldBackend.Models;

public partial class Photo
{
    public int Id { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public virtual ICollection<OfferGallery> OfferGalleries { get; } = new List<OfferGallery>();
}
