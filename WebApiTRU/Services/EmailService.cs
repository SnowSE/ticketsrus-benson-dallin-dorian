using System.Net;
using LibraryTRU.Data.DTOs;
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
    public void SendEmail(EmailInfoDTO emailInfo)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("TRU", "ticketsrus4@gmail.com"));
        message.To.Add(new MailboxAddress("", emailInfo.Email));
        message.Subject = emailInfo.Subject;
        message.Body = new TextPart("plain") {
            Text = emailInfo.Message
            // Add emailInfo.QrCode
        };
        using (var client = new SmtpClient()){
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("ticketsrus4@gmail.com", $"{_config["emailpassword"]}");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}