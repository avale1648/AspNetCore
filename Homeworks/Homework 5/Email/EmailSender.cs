using MimeKit;
using MailKit.Net.Smtp;
namespace Homework_5.Email
{
    public class EmailSender: IEmailSender
    {
        public async Task SendEmailAsync(string senderName, string senderEmail, string smtpServer, int port, string password, string recieverName, string recieverEmail, string subject, string message)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(senderName, senderEmail));
            emailMessage.To.Add(new MailboxAddress(recieverName, recieverEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using var client = new SmtpClient();
            await client.ConnectAsync(smtpServer, port, false);
            await client.AuthenticateAsync(senderEmail, password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
