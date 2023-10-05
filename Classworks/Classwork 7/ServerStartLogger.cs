namespace Classwork_7
{
    public class LaunchLogBackgroundService: BackgroundService
    {
        private readonly ILogger<LaunchLogBackgroundService> logger;
        public LaunchLogBackgroundService(ILogger<LaunchLogBackgroundService> logger, ApplicationLifetime al)
        {
            this.logger = logger ?? throw new ArgumentNullException
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Сервер успешно запущен");
        }
    }
}
