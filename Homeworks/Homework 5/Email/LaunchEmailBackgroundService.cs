using System.Diagnostics;
///
namespace Homework_5.Email
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly ILogger<EmailBackgroundService> logger;
        public EmailBackgroundService(ILogger<EmailBackgroundService> logger)
        {
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromHours(1));
            var stopwatch = Stopwatch.StartNew();
            while(await timer.WaitForNextTickAsync(stoppingToken))
            {
                await new EmailService().SendEmailAsync("avale1648@gmail.com", "Сервер", $"Сервер был запущен и работает {stopwatch.Elapsed}");
            }   
        }
    }
}
