using LibraryTRU.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTRU.IServices;

public interface ITicketService
{
    public Task<IEnumerable<Ticket>> GetAll();
    public Task<Ticket> AddTicket(string email, int concertId);
    public Task ScanTicket(string qrHash);
    public Task<Ticket> GetSingleTicket(string email);
    public Task DeleteTicket(int id);
    public Task<ActionResult<Ticket>> GetTicketById(int id);
}
