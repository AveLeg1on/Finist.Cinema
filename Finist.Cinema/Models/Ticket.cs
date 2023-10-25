using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int Idfilm { get; set; }

    public int TotalSum { get; set; }

    public int IdmovieSchedule { get; set; }

    public int Idviewer { get; set; }

    public int? Idrooms { get; set; }

    public virtual Movie IdfilmNavigation { get; set; }

    public virtual MovieSchedule IdmovieScheduleNavigation { get; set; }

    public virtual Viewer IdviewerNavigation { get; set; }

    public virtual ICollection<Food> Idfoods { get; set; } = new List<Food>();
}
