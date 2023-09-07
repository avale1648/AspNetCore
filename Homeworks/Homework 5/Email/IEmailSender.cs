namespace Homework_5.Email
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string senderName, string senderEmail, string smtpServer, int port, string password, string recieverName, string recieverEmail, string subject, string message);
    }
}
