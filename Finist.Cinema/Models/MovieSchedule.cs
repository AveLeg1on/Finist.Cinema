using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class MovieSchedule
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public DateTime ScreeningTime { get; set; }

    public int? Idroom { get; set; }

    public int Duration { get; set; }

    public virtual ViewingRoom IdroomNavigation { get; set; }

    public virtual Movie Movie { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
