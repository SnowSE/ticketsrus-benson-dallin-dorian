using Microsoft.EntityFrameworkCore;

namespace WebApiTRU.Services
{
    public class UpdateTicketService
    {
        PostgresContext _ticketContext;
        public UpdateTicketService(IDbContextFactory<PostgresContext> contxtFact)
        {
            _ticketContext = contxtFact.CreateDbContext();
        }


    }
}
