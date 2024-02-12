using System.Net;
using System.Net.Mail;
using LibraryTRU.IServices;

namespace WebApiTRU.Email;

public class EmailSender : IEmailService
{
    private readonly IConfiguration _config;
    public EmailSender(IConfiguration config)
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
            Credentials = new NetworkCredential("ticketsrus@gmail.com", emailpassword)
        };

        return client.SendMailAsync(
            new MailMessage(from: "your.email@live.com",
                            to: email,
                            subject,
                            message
                            ));
    }
}