
namespace WebApi0.BackgroundServices
{
    public class LogSpammer(ILogger<LogSpammer> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                logger.LogInformation($"This is log: {DateTime.Now}");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}