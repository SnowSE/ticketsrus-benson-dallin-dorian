using LibraryTRU.Data.DTOs;

namespace WebApiTRU.Controllers;

[ApiController]
[Route("email")]
public class EmailController : Controller
{
    private readonly IEmailService emailService;

    public EmailController(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailInfoDTO model)
    {
        if (model == null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await emailService.SendEmailAsync(model.Email, model.Subject, model.Message);

        return Ok();
    }
}