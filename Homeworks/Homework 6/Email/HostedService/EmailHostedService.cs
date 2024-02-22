using Homework_6.Email.Config;
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
        private int _timeSpan = 1;
        private TimeUnit _timeUnit = TimeUnit.Minute;
        private SmtpEmailSender _sender;
        //public SmtpEmailSender(string host, int port, SecureSocketOptions options, string login, string password);
        //public async Task SendEmailAsync(string senderName, string senderEmail, string recieverName, string recieverEmail, string subject, string body, CancellationToken token);
        public EmailHostedService(SmtpConfig config)
        {
            _sender = new SmtpEmailSender(config);
        }
        private async Task SendEmailEveryTimeSpanAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await _sender.SendEmailAsync(cancellationToken);
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
            await _sender.DisposeAsync();
        }
    }
}
