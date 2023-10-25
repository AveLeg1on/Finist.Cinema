using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class Actor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Secondname { get; set; }

    public DateTime? DateBirthday { get; set; }

    public string Gender { get; set; }

    public virtual ICollection<Movie> Idfilms { get; set; } = new List<Movie>();
}
