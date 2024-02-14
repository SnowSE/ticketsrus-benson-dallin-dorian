using LibraryTRU.IServices;
using System.Net.Http.Json;
using LibraryTRU.Data;

namespace MauiTRU.Services
{
    public class MauiTicketService(HttpClient _hpClient) : ITicketService
    {
        public async Task AddTicket(string email, int concertId)
        {
            await _hpClient.PostAsJsonAsync("api/ticket/new", (email, concertId));
        }

        public Task DeleteTicket(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _hpClient.GetFromJsonAsync<IEnumerable<Ticket>>("api/ticket/getall");
        }

        public async Task<Ticket> GetSingleTicket(string email)
        {
            return await _hpClient.GetFromJsonAsync<Ticket>($"api/ticket/{email}");
        }

        public Task<Ticket> GetTicketById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task ScanTicket(string qrHash)
        {
            await _hpClient.PutAsJsonAsync("api/ticket/scan", qrHash);
        }

        Task<Ticket> ITicketService.AddTicket(string email, int concertId)
        {
            throw new NotImplementedException();
        }
    }
}
