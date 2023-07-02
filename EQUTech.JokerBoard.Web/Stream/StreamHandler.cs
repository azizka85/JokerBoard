using EQUTech.Core.Grpc.Services.JokerBoard.Stream;
using EQUTech.Core.Tasks.Handlers;

namespace EQUTech.JokerBoard.Web.Stream;

public sealed class StreamHandler : IPingTaskHandler
{
    private readonly StreamManager _streamManager;

    public StreamHandler(StreamManager streamManager)
    {
        _streamManager = streamManager ?? throw new ArgumentNullException(nameof(streamManager));
    }   

    public async Task PingAsync(CancellationToken cancellationToken)
    {
        await _streamManager.SendAsync
        (
            _streamManager.Streams.Select(item => item.Value.ResponseStream),
            new CallResponse(),
            cancellationToken
        );
    }
}