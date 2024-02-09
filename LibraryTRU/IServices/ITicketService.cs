using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryTRU.Data;

namespace LibraryTRU.IServices;

public interface ITicketService
{
    public Task<IEnumerable<Ticket>> GetAll();
    public Task AddTicket(string email, int concertId);
    public Task ScanTicket(string qrHash);
}
