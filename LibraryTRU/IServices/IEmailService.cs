

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LibraryTRU.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
