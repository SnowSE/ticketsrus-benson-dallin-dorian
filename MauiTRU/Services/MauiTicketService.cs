using LibraryTRU.IServices;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApiTRU.Data;

namespace MauiTRU.Services
{
    public class MauiTicketService(HttpClient _hpClient) : ITicketService
    {
        public async Task AddTicket(string email, int concertId)
        {
            await _hpClient.PostAsJsonAsync("api/ticket/new", (email, concertId));
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _hpClient.GetFromJsonAsync<IEnumerable<Ticket>>("api/ticket/getall");
        }

        public async Task ScanTicket(string qrHash)
        {
            await _hpClient.PutAsJsonAsync("api/ticket/scan", qrHash);
        }
    }
}
