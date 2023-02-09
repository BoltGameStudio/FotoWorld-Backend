using System;
using System.Collections.Generic;

namespace FotoWorldBackend.Models;

public partial class OperatorService
{
    public int Id { get; set; }

    public bool Photo { get; set; }

    public bool DronePhoto { get; set; }

    public bool DroneFilm { get; set; }

    public bool Filming { get; set; }

    public virtual ICollection<Operator> Operators { get; } = new List<Operator>();
}
