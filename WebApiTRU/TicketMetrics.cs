using System.Diagnostics.Metrics;

public class TicketMetrics
{
    private readonly Counter<int> _ticketsSold;

    public TicketMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("Ticket.Store");
        _ticketsSold = meter.CreateCounter<int>("ticket.store.tickets_sold");
    }

    public void TicketsSold(int quantity)
    {
        _ticketsSold.Add(quantity);
    }
}