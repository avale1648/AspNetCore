using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
namespace Homework_5.Email
{
    public interface IEmailSender : IAsyncDisposable
    {
        public bool isConnected { get; }
        public bool isAuthenticated { get; }
        public Task SendEmailAsync(string senderName, string senderEmail, string recieverName, string recieverEmail, string subject, string body, CancellationToken token);
        public Task ConnectAsync(string host, int port, bool useSsl, CancellationToken token);
        public Task AuthenticateAsync(string email, string password, CancellationToken token);
        public Task DisconnectAsync(bool quit);
    }
}
