using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Turnero.Infrastructure.Email
{
    /// <summary>
    /// Implementación de IEmailService usando MailKit.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            var message = new MimeMessage();

            // De: configurado en appsettings.json
            message.From.Add(new MailboxAddress(
                _config["Email:FromName"],
                _config["Email:FromAddress"]
            ));

            // Para:
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            // Cuerpo HTML
            var builder = new BodyBuilder { HtmlBody = htmlBody };
            message.Body = builder.ToMessageBody();

            // Conexión SMTP
            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["Email:SmtpHost"],
                int.Parse(_config["Email:SmtpPort"] ?? "587"),
                SecureSocketOptions.StartTls
            );
            await client.AuthenticateAsync(
                _config["Email:SmtpUser"],
                _config["Email:SmtpPass"]
            );
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
