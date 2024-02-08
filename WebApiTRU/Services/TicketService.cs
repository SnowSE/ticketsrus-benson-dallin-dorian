using LibraryTRU.IServices;
using Microsoft.EntityFrameworkCore;
using WebApiTRU.Data;
using WebApiTRU.Exceptions;

namespace WebApiTRU.Services
{
    public class TicketService : ITicketService
    {
        PostgresContext _ticketContext;
        public TicketService(PostgresContext _pgContext)
        {
            _ticketContext = _pgContext;
        }

        public async Task<Ticket> AddTicket(string email, int concertId)
        {
            Ticket ticket = MakeTicketFrom(email, concertId);
            await _ticketContext.Tickets.AddAsync(ticket);
            await _ticketContext.SaveChangesAsync();
            return ticket;
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

        private Ticket MakeTicketFrom(string email, int concertId)
        {
            Ticket ticket = new Ticket()
            {
                Email = email,
                ConcertId = concertId,
                Qrhash = GenerateTicketHash()
            };

            return ticket;
        }

        private string GenerateTicketHash()
        {
            string hash;
            char[] chars = new char[16];

            return chars.ToString();
            
        }
    
    }
}
