namespace Alaska.Services;

public class AlaskaServer : BackgroundService
{
    public AlaskaServer(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger<AlaskaServer>();
    }

    public ILogger Logger { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("ServiceA is starting.");

        stoppingToken.Register(() => Logger.LogInformation("ServiceA is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            Logger.LogInformation("ServiceA is doing background work.");

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        Logger.LogInformation("ServiceA has stopped.");
    }
}