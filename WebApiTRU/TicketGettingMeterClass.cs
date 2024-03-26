using System.Diagnostics.Metrics;
using WebApiTRU.Components;

public static class TicketGettingMeterClass
{
    static Meter meter = new("TicketGetting", "1.0.0");
    public static Counter<int> counter = meter.CreateCounter<int>("a_ticket_getting_counter");
}