namespace EQUTech.Core.Tasks.Handlers;

public interface IPingTaskHandler
{
    Task PingAsync(CancellationToken cancellationToken);
}
