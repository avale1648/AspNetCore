using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
//
namespace Homework_6.Email
{
    public class SmtpEmailSender : IEmailSender, IAsyncDisposable
    {
        private readonly SmtpClient _client;
        public SmtpEmailSender(string host, int port, SecureSocketOptions options, string login, string password) 
        {
            _client = new();
            _client.Connect(host, port, options);
            _client.Authenticate(login, password);
        }
        public async Task SendEmailAsync(string senderName, string senderEmail, string recieverName, string recieverEmail, string subject, string body, CancellationToken token)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.From.Add(new MailboxAddress(recieverName, recieverEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
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
