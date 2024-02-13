using System.Net;
using MailKit.Net;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
namespace WebApiTRU.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    public void SendEmail(string email, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("TRU", "ticketsrus3@gmail.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = subject;
        message.Body = new TextPart("plain") {
            Text = body
        };
        using (var client = new SmtpClient()){
            client.Connect("smtp.server.come", 587, false);
            client.Authenticate("ticketsrus3@gmail.com", $"{_config["googlepassword"]}");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}