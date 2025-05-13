using System;
using System.Threading.Tasks;
using Abstracciones.Modelos;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Servicios
{
    public class EnviarCorreoService
    {
        private readonly MailSettings _mailSettings;

        public EnviarCorreoService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task EnviarCorreo(string destinatario, string asunto, string contenido)
        {
            var message = new MimeMessage();
            // Corregido: Añadir nombre y correo del remitente
            message.From.Add(new MailboxAddress(_mailSettings.FromName, _mailSettings.FromEmail));
            // Corregido: Añadir nombre y correo del destinatario
            message.To.Add(new MailboxAddress(destinatario, destinatario));
            message.Subject = asunto;

            var body = new TextPart("html") { Text = contenido };  // Corregido
            message.Body = body;

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.SmtpUser, _mailSettings.SmtpPassword);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
