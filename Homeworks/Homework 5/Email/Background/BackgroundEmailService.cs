using Homework_5.MemoryChecker;
namespace Homework_5.Email.Background
{
    public class BackgroundEmailService: BackgroundService
    {
        private readonly IEmailSender emailSender;
        private readonly IMemoryChecker memoryChecker;
        private readonly TimeSpan timeout;
        private readonly IConfiguration config;
        public BackgroundEmailService(IEmailSender emailSender, IMemoryChecker memoryChecker, TimeSpan timeout, IConfiguration config)
        {
            this.emailSender = emailSender;
            this.memoryChecker = memoryChecker;
            this.timeout = timeout;
            this.config = config;
        }
        protected override async Task ExecuteAsync(CancellationToken token)
        {
            await emailSender.ConnectAsync("", 25, true, token);
            await emailSender.AuthenticateAsync("", "", token);
            while(!token.IsCancellationRequested)
            {
                try
                {
                    await emailSender.SendEmailAsync("", "", "", "", "", "", token);
                    await Task.Delay(timeout, token);
                }
                catch (Exception ex)
                {
                    await emailSender.DisconnectAsync(true);
                    await emailSender.DisposeAsync();
                }
            }
        }
    }
}
