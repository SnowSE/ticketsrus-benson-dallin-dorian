

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LibraryTRU.IServices
{
    public interface IEmailService
    {
        void SendEmail(string email, string subject, string message);
    }
}
