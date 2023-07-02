using EQUTech.Core.Tasks.Handlers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EQUTech.Core.Tasks;

public sealed class PingTask : IHostedService
{
    private readonly TimeSpan _pingDelay;

    private readonly List<IPingTaskHandler> _handlers;

    private CancellationTokenSource? cancellationTokenSource;

    private readonly ILogger<PingTask>? _logger;

    public PingTask(TimeSpan pingDelay, List<IPingTaskHandler> handlers, ILogger<PingTask>? logger)
    {
        _pingDelay = pingDelay;

        _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));

        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        cancellationTokenSource?.Cancel();

        cancellationTokenSource = new CancellationTokenSource();

        _ = PeriodicPingAsync(cancellationTokenSource.Token);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        cancellationTokenSource?.Cancel();

        return Task.CompletedTask;
    }

    public async Task PeriodicPingAsync(CancellationToken cancellationToken)
    {
        _logger?.LogInformation("Ping task started");

        while (!cancellationToken.IsCancellationRequested)
        {
            foreach (var handler in _handlers)
            {
                try
                {
                    await handler.PingAsync(cancellationToken);
                }
                catch (Exception exception)
                {
                    _logger?.LogWarning(exception, "Error on handling ping, {FullName}", handler.GetType().FullName);
                }
            }

            await Task.Delay(_pingDelay, cancellationToken);
        }

        _logger?.LogInformation("Ping task stopped");
    }
}
