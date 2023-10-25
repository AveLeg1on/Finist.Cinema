using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class Country
{
    public int Id { get; set; }

    public string NameCountry { get; set; }

    public virtual ICollection<Movie> Idfilms { get; set; } = new List<Movie>();
}
