using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class ActorForMovie
{
    public string FilmName { get; set; }

    public byte[] Poster { get; set; }

    public string Name { get; set; }

    public string Secondname { get; set; }

    public int Id { get; set; }
}
