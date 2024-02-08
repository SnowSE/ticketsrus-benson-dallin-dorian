using LibraryTRU.IServices;
using Microsoft.EntityFrameworkCore;
using WebApiTRU.Data;

namespace WebApiTRU.Services
{
    public class ConcertService : IConcertService
    {
        private readonly PostgresContext _concertcontext;
        public ConcertService(PostgresContext newDB)
        {
            _concertcontext = newDB;
        }

        public async Task<IEnumerable<Concert>> GetAll()
        {
            return await _concertcontext.Concerts.ToListAsync();
        }
    }
}
