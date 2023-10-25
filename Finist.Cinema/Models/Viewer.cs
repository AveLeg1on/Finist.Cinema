using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class Viewer
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Secondname { get; set; }

    public string? Patronymic { get; set; }

    public long Telephone { get; set; }

    public string Email { get; set; }

    public DateTime? DateBirthday { get; set; }

    public byte[]? Photo { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
