using System.Net;
using System.Net.Mail;

namespace WebApiTRU.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var emailpassword = _config["googlepassword"];
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("ticketsrus3@gmail.com", emailpassword)
        };

        return client.SendMailAsync(
            new MailMessage(from: "ticketsrus3@gmail.com",
                            to: email,
                            subject,
                            message
                            ));
    }
}