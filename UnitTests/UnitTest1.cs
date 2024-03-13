using FluentAssertions;
using LibraryTRU.Data.DTOs;

namespace UnitTests;

public class UnitTest1
{
    [Fact]
    public void TicketDTOWorks()
    {
        TicketDTO td = new() { Email = "FakeEmail@fake.com", ConcertId = 4 };
        td.ConcertId.Should().Be(4);
    }
}