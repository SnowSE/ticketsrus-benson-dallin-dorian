using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTRU.Data;

namespace MauiTRU;

public class TRUDatabase
{
    SQLiteAsyncConnection Database;
    public TRUDatabase()
    {
        
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await Database.CreateTableAsync<Ticket>();
        await Database.CreateTableAsync<Concert>();
    }

    public async Task<List<Ticket>> GetTicketsAsync()
    {
        await Init();
        return await Database.Table<Ticket>().ToListAsync();
    }
    public async Task<List<Concert>> GetConcertsAsync()
    {
        await Init();
        return await Database.Table<Concert>().ToListAsync();
    }

    //public async Task<Ticket> CreateTicketAsync(string email, int concertId)
    //{
    //    await Init();

    //    Ticket ticket = new Ticket()
    //    {
    //        Email = email,
    //        ConcertId = concertId,
    //        Qrhash = GenerateTicketHash()
    //    };

    //    await Database.InsertAsync(ticket);

    //    return ticket;
    //}

    //private string GenerateTicketHash()
    //{
    //    string hash;
    //    char[] chars = new char[16];

    //    return chars.ToString();
    //}

    public async Task ScanTicketAsync(string qrHash)
    {
        await Init();
        var ticket = await Database.Table<Ticket>().Where(t => t.Qrhash == qrHash).FirstOrDefaultAsync();
        ticket.Timescanned = DateTime.Now;
        await Database.UpdateAsync(ticket);
    }




}
