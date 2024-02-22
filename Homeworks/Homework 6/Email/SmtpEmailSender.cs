using Homework_6.Email.Config;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
//
namespace Homework_6.Email
{
    public class SmtpEmailSender : IEmailSender, IAsyncDisposable
    {
        private readonly SmtpConfig _config;
        private readonly SmtpClient _client;
        public SmtpEmailSender(SmtpConfig config) 
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _client = new SmtpClient();
            _client.Connect(_config.Host, _config.Port, SecureSocketOptions.Auto);
            _client.Authenticate(_config.Username, _config.Password);
        }
        public async Task SendEmailAsync(CancellationToken token)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config.SenderName, _config.SenderEmail));
            message.From.Add(new MailboxAddress(_config.ReceiverName, _config.ReceiverEmail));
            message.Subject = _config.Subject;
            message.Body = new TextPart("plain")
            {
                Text = _config.Body
            };
            await _client.SendAsync(message, token);
        }
        public async Task Dispose()
        {
            await _client.DisconnectAsync(true);
            _client.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
