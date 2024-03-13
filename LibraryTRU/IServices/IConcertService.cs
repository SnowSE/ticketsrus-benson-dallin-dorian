using LibraryTRU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTRU.IServices
{
    public interface IConcertService
    {
        public Task<IEnumerable<Concert>> GetAll();
        //public Task<Concert> Add(string email, int concertId);
    }
}