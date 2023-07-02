namespace EQUTech.Core.Utils.Extensions;

public static class CancellationTokenExtensions
{
    public static Task AsTask(this CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<bool>();

        cancellationToken.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);

        return tcs.Task;
    }
}
