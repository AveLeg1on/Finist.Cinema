using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class Worker
{
    public int Id { get; set; }

    public string Secondname { get; set; }

    public string Name { get; set; }

    public string Patronymic { get; set; }

    public int PassportSeries { get; set; }

    public int PassportNumber { get; set; }

    public long Telephone { get; set; }

    public byte[] Photo { get; set; }

    public virtual ICollection<ViewingRoom> IdnumberViewingRooms { get; set; } = new List<ViewingRoom>();
}
