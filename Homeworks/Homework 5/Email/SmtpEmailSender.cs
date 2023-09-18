using MailKit.Net.Smtp;
using MimeKit;

namespace Homework_5.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private SemaphoreSlim semaphore = new(1, 1);
        private readonly ILogger logger;
        private readonly SmtpClient client;
        private bool isDisposed;
        public bool isConnected => client.IsConnected;
        public bool isAuthenticated => client.IsAuthenticated;
        public SmtpEmailSender(ILogger logger)
        {
            isDisposed = false;
            this.logger = logger;
            client = new SmtpClient();
        }
        public async Task ConnectAsync(string host, int port, bool useSsl, CancellationToken token)
        {
            if (!client.IsConnected)
            {
                await semaphore.WaitAsync(token);
                await client.ConnectAsync(host, port, useSsl, token);
                semaphore.Release();
            }
            else
            {
                throw new Exception("Сервис по отправке эл. почты по протоколу SMTP уже подключен.");
            }
        }
        public async Task AuthenticateAsync(string email, string password, CancellationToken token)
        {
            if (!client.IsAuthenticated)
            {
                await semaphore.WaitAsync(token);
                await client.AuthenticateAsync(email, password, token);
                semaphore.Release();
            }
            else
            {
                throw new Exception("Сервис по отправке эл. почты по протоколу SMTP уже авторизован.");
            }
        }
        public async Task DisconnectAsync(bool quit)
        {
            if (client.IsConnected)
            {
                await semaphore.WaitAsync();
                await client.DisconnectAsync(quit);
                semaphore.Release();
            }
            else
            {
                throw new Exception("Сервис по отправке эл. почты по протоколу SMTP уже отключен.");
            }
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
        public async Task DisposeAsync(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (client.IsConnected)
                {
                    await semaphore.WaitAsync();
                    await client.DisconnectAsync(true);
                    semaphore.Release();
                }
                client.Dispose();
            }
            isDisposed = true;
        }
        public async Task SendEmailAsync(string senderName, string senderEmail, string recieverName, string recieverEmail, string subject, string body, CancellationToken token)
        {
            if (!client.IsConnected)
            {
                throw new Exception("Сервис по отправке эл. почты по протоколу SMTP не подключен.");
            }
            if (!client.IsAuthenticated)
            {
                throw new Exception("Сервис по отправке эл. почты по протоколу SMTP не авторизован.");
            }
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(senderName, senderEmail));
            mimeMessage.To.Add(new MailboxAddress(recieverName, recieverEmail));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = body
            };
            try
            {
                await semaphore.WaitAsync(token);
                var response = await client.SendAsync(mimeMessage, token);
                logger.LogInformation("Ответ SMTP-сервера: " + response);
                semaphore.Release();
            }
            catch (Exception ex)
            {
                logger.LogInformation("Ответ SMTP-сервера: сообщение не доставлено.");
            }
        }
    }
}
