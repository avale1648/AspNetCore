using MimeKit;
using MailKit.Net.Smtp;
namespace Homework_5.Email
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.beget.com", 25, false);
            await client.AuthenticateAsync("asp2023pv112@rodion-m.ru", "mL6%mbxR");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
