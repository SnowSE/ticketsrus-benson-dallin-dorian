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
        int id = ticket.Id;

        //make sure the ticket hasnt been scanned
        var testResult = await client.GetFromJsonAsync<Ticket>($"api/ticket/{id}");
        testResult.Timescanned.Should().BeNull();

        //Act: Scan the ticket
        var qrHash = ticket.Qrhash;
        await client.PutAsJsonAsync("/api/ticket/scan", qrHash);

        //Assert: get the ticket and see if it was scanned
        var testResult2 = await client.GetFromJsonAsync<Ticket>($"api/ticket/{id}");
        testResult2.Timescanned.Should().NotBeNull();
     
    }

    [Fact]
    public async void FailedScanDoesntUpdateDatabase()
    {
        //Arrange: create new ticket
        string testEmail = "test@example.com";
        TicketDTO data = new() { Email = testEmail, ConcertId = 1 };
        var result = await client.PostAsJsonAsync("/api/ticket/new", data);
        var ticket = result.Content.ReadFromJsonAsync<Ticket>().Result;
        int id = ticket.Id;

        //make sure the ticket hasnt been scanned
        var testResult = await client.GetFromJsonAsync<Ticket>($"api/ticket/{id}");
        testResult.Timescanned.Should().BeNull();

        //Act: Scan the ticket with a bogus hash
        await client.PutAsJsonAsync("/api/ticket/scan", "123");

        //Assert: get the ticket and make sure it wasn't scanned
        var testResult2 = await client.GetFromJsonAsync<Ticket>($"api/ticket/{id}");
        testResult2.Timescanned.Should().BeNull();
    }

    [Fact]
    public void APIAddressChangeRefreshesLocalDb()
    {

    }


}