using System;
using System.Collections.Generic;

namespace Finist.Cinema.Models;

public partial class ViewWorkerToRoom
{
    public int Id { get; set; }

    public int NumberViewingRoom { get; set; }

    public int? Idworker { get; set; }

    public string Patronymic { get; set; }

    public string Secondname { get; set; }
}
