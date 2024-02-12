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
        //create new ticket
        string testEmail = "test@example.com";
        TicketDTO data = new() {Email =  testEmail, ConcertId = 1};
        var ticket = await client.PostAsJsonAsync("/api/ticket/new", data);
        ticket.Content.ReadFromJsonAsync<Ticket>().Result.Email.Should().Be(testEmail);
     
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