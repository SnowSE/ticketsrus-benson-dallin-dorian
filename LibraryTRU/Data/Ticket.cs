using SQLite;

namespace WebApiTRU.Data;

public partial class Ticket
{
    [PrimaryKey]
    public int Id { get; set; }

    public string Qrhash { get; set; } = null!;

    public string? Email { get; set; }

    public int? ConcertId { get; set; }

    public DateTime? Timescanned { get; set; }

    public virtual Concert? Concert { get; set; }
}
