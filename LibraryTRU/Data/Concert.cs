using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LibraryTRU.Data;

public partial class Concert
{
    [PrimaryKey]
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string EventName { get; set; } = null!;

    public string? Description { get; set; }

    [OneToMany]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
