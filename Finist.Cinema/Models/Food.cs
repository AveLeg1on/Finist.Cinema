using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class Food
{
    public int Id { get; set; }

    public string NameFood { get; set; }

    public int Cost { get; set; }

    public byte[] Photo { get; set; }

    public virtual ICollection<Ticket> Idtickets { get; set; } = new List<Ticket>();
}
