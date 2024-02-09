namespace WebApiTRU.Controllers;

[ApiController]
[Route("/api/[Controller]")]
public class TicketController : Controller
{
    ITicketService _ts;
    public TicketController(ITicketService ticketService)
    {
        _ts = ticketService;
    }

    [HttpGet("getall")]
    public async Task<IEnumerable<Ticket>> GetAll()
    {
        return await _ts.GetAll();
    }

    [HttpPost("new")]
    public async Task PostTicket([FromBody] (string email, int concertId) emailAndConcertId)
    {
        await _ts.AddTicket(emailAndConcertId.email, emailAndConcertId.concertId);
    }

    [HttpPut("scan")]
    public async Task ScanTicket([FromBody]string qrHash)
    {
        try
        {
            await _ts.ScanTicket(qrHash);
        }
        catch (TicketNotFoundException tex)
        {
            Response.StatusCode = 410;
        }
        catch
        {
            Response.StatusCode = 500;
        }
    }

}
