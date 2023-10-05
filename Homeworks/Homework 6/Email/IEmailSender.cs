using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
namespace Homework_5.Email
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string senderName, string senderEmail, string recieverName, string recieverEmail, string subject, string body, CancellationToken token);
    }
}
