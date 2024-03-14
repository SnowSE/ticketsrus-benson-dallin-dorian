namespace LibraryTRU.Data;

public partial class Ticket : Object
{
    public int Id { get; set; }

    public string Qrhash { get; set; } = null!;

    public string? Email { get; set; }

    public int? ConcertId { get; set; }

    public DateTime? Timescanned { get; set; }

    public virtual Concert? Concert { get; set; }
}