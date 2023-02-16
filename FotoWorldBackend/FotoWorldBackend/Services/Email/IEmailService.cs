using FotoWorldBackend.Models;


namespace FotoWorldBackend.Services.Email
{
    public interface IEmailService
    {
        public void SendEmail(EmailModel request);
    }
}
