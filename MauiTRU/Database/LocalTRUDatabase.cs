using SQLite;
using LibraryTRU.Data;
using System.Net.Http.Json;

namespace MauiTRU.Database;

public class LocalTRUDatabase
{
    SQLiteAsyncConnection Database;
    IDbPath _ifs;
    HttpClient _client;
    public LocalTRUDatabase(IDbPath ifs, HttpClient client)
    {
        _ifs = ifs;
        _client = client;
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Path.Combine(_ifs.Directory, Constants.DatabaseFilename), Constants.Flags);
        await Database.CreateTableAsync<Concert>();
        await Database.CreateTableAsync<Ticket>();
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
    public async Task ScanTicketAsync(string qrHash)
    {
        await Init();
        var ticket = await Database.Table<Ticket>().Where(t => t.Qrhash == qrHash).FirstOrDefaultAsync();
        ticket.Timescanned = DateTime.Now;
        await Database.UpdateAsync(ticket);
    }

    public async Task UpdateLocalDbFromMainDb()
    {
        await Init();
        var mainTickets = await _client.GetFromJsonAsync<IEnumerable<Ticket>>("api/ticket/getall");
        var localTickets = await Database.Table<Ticket>().ToListAsync();

        foreach(Ticket mainTicket in mainTickets)
        {
            try 
            { 
                var result = await Database.GetAsync<Ticket>(localTicket => localTicket.Id == mainTicket.Id);

                if(result.Timescanned is not null) // If it has been scanned, update it
                    await Database.UpdateAsync(result);
            }
            catch (SQLiteException) // Main Ticket not found in local db
            {
                await Database.InsertAsync(mainTicket);
            }
        }

        foreach (Ticket localTicket in localTickets)
        {
            if(!mainTickets.Contains(localTicket)) // Delete any tickets that are deleted in the main db
                await Database.DeleteAsync(localTicket);
        }
    }

    public async Task UpdateMainDbFromLocalDb()
    {
        await Init();
        var localTickets = await Database.Table<Ticket>().ToListAsync();
        var mainTickets = await _client.GetFromJsonAsync<IEnumerable<Ticket>>("api/ticket/getall");

        foreach (Ticket localTicket in localTickets)
            if(localTicket.Timescanned is not null) //If the local ticket is scanned
                if (mainTickets.Where(mt => mt.Id == localTicket.Id).Single().Timescanned is not null) // And the main ticket is not scanned
                    await _client.PutAsJsonAsync("api/ticket/scan", localTicket.Qrhash); // scan the main one
    }
}
