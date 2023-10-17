using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
//
namespace Homework_5.Email
{
    public class SmtpEmailSender : IEmailSender, IAsyncDisposable
    {
        //использовать IOptions для конструктора
        private readonly SmtpClient _client;
        public SmtpEmailSender() 
        {
            _client = new();
            _client.Connect("smtp.beget.com", 25, SecureSocketOptions.None);
            _client.Authenticate("asp2022pd011@rodion-m.ru", "6WU4x2be");
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
