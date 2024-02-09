using LibraryTRU.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

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
        //arrange
        var tickets = await client.GetFromJsonAsync<IEnumerable<Ticket>>("api/ticket/getall");

        //act
        //assert
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