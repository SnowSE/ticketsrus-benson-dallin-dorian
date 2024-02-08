using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTRU.Data;

namespace LibraryTRU.IServices
{
    public interface ITicketService
    {
        public Task<IEnumerable<Ticket>> GetAll();
        public Task<Ticket> Add(string email, int concertId);
        public Task ScanTicket(string qrHash);
    }
}
