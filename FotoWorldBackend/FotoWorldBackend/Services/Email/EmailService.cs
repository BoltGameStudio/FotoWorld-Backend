using FotoWorldBackend.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace FotoWorldBackend.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config= config;
        }

        public void SendEmail(EmailModel request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.Recipant));
            email.Subject = request.Subject;
            email.Body= new TextPart(TextFormat.Html) { Text=request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, Convert.ToInt32(_config.GetSection("EmailPort").Value), SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }


        public void SendActivationEmail(User user)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Account Activation";

            //tutaj trzeba skleic ladnego urla
            email.Body = new TextPart(TextFormat.Html) { Text = _config.GetValue<string>("Urls:BackendUrl") };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, Convert.ToInt32(_config.GetSection("EmailPort").Value), SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
