using System.Threading.Tasks;

namespace CareerConnect.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
