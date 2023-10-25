using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class ViewingRoom
{
    public int Id { get; set; }

    public int NumberViewingRoom { get; set; }

    public int? Idworker { get; set; }

    public virtual ICollection<MovieSchedule> MovieSchedules { get; set; } = new List<MovieSchedule>();

    public virtual ICollection<Worker> Idworkers { get; set; } = new List<Worker>();
}
