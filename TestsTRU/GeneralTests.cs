using FluentAssertions;
using LibraryTRU.Data;
using LibraryTRU.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestsTRU
{
    public class GeneralTests : IClassFixture<TRUWebAppFactory>
    {
        HttpClient client;
        public GeneralTests(TRUWebAppFactory factory)
        {
            client = factory.CreateDefaultClient();
        }

        [Fact]
        public async void GetAllTicketsTest()
        {
            var tickets = await client.GetFromJsonAsync<IEnumerable<Ticket>>("api/ticket/getall");
            tickets.Where(o => o.Id == 1).Should().HaveCount(1);
        }

        [Fact]
        public async void PostTicketToDbWithDTOReturnsCreatedTicket()
        {
            string testEmail = "test@example.com";
            TicketDTO data = new() { Email = testEmail, ConcertId = 1 };
            var ticket = await client.PostAsJsonAsync("/api/ticket/new", data);
            ticket.Content.ReadFromJsonAsync<Ticket>().Result.Email.Should().Be(testEmail);
        }
    

    
    }
}
