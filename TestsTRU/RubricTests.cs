using FluentAssertions;
using LibraryTRU.Data;
using LibraryTRU.Data.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace TestsTRU;


public class RubricTests : IClassFixture<TRUWebAppFactory>
{
    HttpClient client;
    public RubricTests(TRUWebAppFactory factory)
    {
        client = factory.CreateDefaultClient();
    }

    [Fact]
    public async void SuccessfulScanUpdatesDatabase()
    {
        //Arrange: create new ticket
        string testEmail = "test@example.com";
        TicketDTO data = new() {Email =  testEmail, ConcertId = 1};
        var result = await client.PostAsJsonAsync("/api/ticket/new", data);
        var ticket = result.Content.ReadFromJsonAsync<Ticket>().Result;

        //Act: Scan the ticket
        var qrHash = ticket.Qrhash;
        await client.PutAsJsonAsync("/api/ticket/scan", qrHash);

        //Assert: get the ticket and see if it was scanned
        int id = ticket.Id;
        var testResult = await client.GetFromJsonAsync<Ticket>($"api/ticket/{id}");
        testResult.Timescanned.Should().NotBeNull();
     
    }

    [Fact]
    public void FailedScanDoesntUpdateDatabase()
    {

    }

    [Fact]
    public void APIAddressChangeRefreshesLocalDb()
    {

    }


}