using Homework_6.Util;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Tls;
using System;
//
namespace Homework_6.Email.HostedService
{
    public class EmailHostedService : IHostedService
    {
        private int _timeSpan;
        private TimeUnit _timeUnit;
        private SmtpEmailSender _sender;
        private string _senderName;
        private string _senderEmail;
        private string _recieverName;
        private string _recieverEmail;
        private string _subject;
        private string _body;
        //public SmtpEmailSender(string host, int port, SecureSocketOptions options, string login, string password);
        //public async Task SendEmailAsync(string senderName, string senderEmail, string recieverName, string recieverEmail, string subject, string body, CancellationToken token);
        public EmailHostedService()
        {
            _timeSpan = 1;
            _timeUnit = TimeUnit.Minute;
            _sender = new("smtp.beget.com", 25, SecureSocketOptions.Auto, "asp2022pd011@rodion-m.ru", "6WU4x2be");
            _senderName = "Сервер";
            _senderEmail = "asp2022pd011@rodion-m.ru";
            _recieverName = "avale1648";
            _recieverEmail = "avale1648@gmail.com";
            _subject = "Сервер запущен";
            _body = "Сервер запущен";
        }
        public EmailHostedService(int timeSpan, TimeUnit timeUnit, string host, int port, SecureSocketOptions options, string login, string password, string senderName, string senderEmail, string recieverName, string recieverEmail, string subject, string body)
        {
            _timeSpan = timeSpan;
            _timeUnit = timeUnit;
            _sender = new(host, port, options, login, password);
            _senderName = senderName;
            _senderEmail = senderEmail;
            _recieverName = recieverName;
            _recieverEmail = recieverEmail;
            _subject = subject;
            _body = body;
        }

        private async Task SendEmailEveryTimeSpanAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await _sender.SendEmailAsync(_senderName, _senderEmail, _recieverName, _recieverEmail, _subject, _body, cancellationToken);
                }
                catch(Exception ex)
                {
                    //
                }
                var timeSpan = TimeSpan.Zero;
                if(_timeUnit == TimeUnit.Second)
                {
                    timeSpan = TimeSpan.FromSeconds(_timeSpan);
                }
                if(_timeUnit == TimeUnit.Minute)
                {
                    timeSpan = TimeSpan.FromMinutes(_timeSpan);
                }
                if(_timeUnit == TimeUnit.Hour)
                {
                    timeSpan = TimeSpan.FromHours(_timeSpan);
                }
                await Task.Delay(timeSpan.Milliseconds, cancellationToken);
            }
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await SendEmailEveryTimeSpanAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await SendEmailEveryTimeSpanAsync(cancellationToken);
        }
    }
}
