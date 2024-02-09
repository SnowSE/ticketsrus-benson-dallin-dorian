using LibraryTRU.Data;
using LibraryTRU.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiTRU.Services
{
    public class MauiConcertService : IConcertService
    {
        HttpClient _concertClient;
        public MauiConcertService(HttpClient concertclient)
        {
            _concertClient = concertclient;
        }

        public async Task<IEnumerable<Concert>> GetAll()
        {
            return await _concertClient.GetFromJsonAsync<IEnumerable<Concert>>("api/concert/getall");
        }
    }
}
