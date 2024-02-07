using System;
using System.Collections.Generic;

namespace WebApiTRU.Data;

public partial class Concert
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string EventName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
