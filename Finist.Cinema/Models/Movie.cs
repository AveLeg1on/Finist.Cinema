using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string FilmName { get; set; }

    public string Budget { get; set; }

    public string CompanyPublisher { get; set; }

    public DateTime DateRelease { get; set; }

    public string Director { get; set; }

    public double? Rating { get; set; }

    public byte[] Poster { get; set; }

    public string Description { get; set; }

    public virtual ICollection<MovieSchedule> MovieSchedules { get; set; } = new List<MovieSchedule>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Actor> Idactors { get; set; } = new List<Actor>();

    public virtual ICollection<Country> Idcountries { get; set; } = new List<Country>();
}
