using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
namespace Homework_6.Email
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(CancellationToken token);
    }
}
