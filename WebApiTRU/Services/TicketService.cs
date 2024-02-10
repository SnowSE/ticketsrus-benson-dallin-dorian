using LibraryTRU.Data;
using Microsoft.EntityFrameworkCore;
namespace WebApiTRU.Services;

public class TicketService : ITicketService
{
    PostgresContext _ticketContext;
    public TicketService(PostgresContext _pgContext)
    {
        var strin = _pgContext.Database.GetConnectionString();
        _ticketContext = _pgContext;
    }

    public async Task AddTicket(string email, int concertId)
    {
        Ticket ticket = new Ticket()
        {
            Email = email,
            ConcertId = concertId,
            Qrhash = await GenerateTicketHash()
        };

        await _ticketContext.Tickets.AddAsync(ticket);
        await _ticketContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Ticket>> GetAll()
    {
        return await _ticketContext.Tickets.ToListAsync();
    }

    public async Task ScanTicket(string qrHash) 
    {
        try
        {
            Ticket target = await _ticketContext.Tickets.Where(qr => qr.Qrhash == qrHash).SingleAsync();
            target.Timescanned = DateTime.Now;
            _ticketContext.Update(target);
            await _ticketContext.SaveChangesAsync();
        }
        catch (ArgumentNullException e)
        {
            throw new TicketNotFoundException();
        }
    }

    async Task<string> GenerateTicketHash()
    {
        char[] hash = new char[16];

        for (int i = 0; i < 16; i++)
            hash[i] = (char)Random.Shared.Next(33, 127);

        IEnumerable<Ticket> tickets = await GetAll();

        if(tickets.Where(t => t.Qrhash == hash.ToString()).Count() > 0)
            return await GenerateTicketHash();

        return new string(hash);
    }
}
