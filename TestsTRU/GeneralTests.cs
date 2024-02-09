using FluentAssertions;
using LibraryTRU.Data;
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
    }
}
