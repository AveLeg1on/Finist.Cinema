using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class MoviesWithActorsAndCountry
{
    public int Id { get; set; }

    public string NameCountry { get; set; }

    public int Idcountry { get; set; }

    public int Idfilm { get; set; }

    public int Idmovies { get; set; }

    public string FilmName { get; set; }

    public string Budget { get; set; }

    public string CompanyPublisher { get; set; }

    public int? Idcast { get; set; }

    public DateTime? DateRelease { get; set; }

    public string Director { get; set; }

    public int? IdfilmingLocations { get; set; }

    public double? Rating { get; set; }

    public byte[] Poster { get; set; }

    public string Description { get; set; }

    public int? Cost { get; set; }

    public int IdactorsInactors { get; set; }

    public string Name { get; set; }

    public string Secondname { get; set; }

    public DateTime? DateBirthday { get; set; }

    public string Gender { get; set; }

    public int Idactor { get; set; }

    public int IdfilmInactors { get; set; }
}
