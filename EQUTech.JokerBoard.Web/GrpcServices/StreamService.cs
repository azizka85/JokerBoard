using EQUTech.Core.Grpc.Services.JokerBoard.Stream;
using EQUTech.Core.Utils.Extensions;
using EQUTech.JokerBoard.Web.Stream;
using Grpc.Core;
using StreamServiceGrpc = EQUTech.Core.Grpc.Services.JokerBoard.Stream.Service.ServiceBase;

namespace EQUTech.JokerBoard.Web.GrpcServices;

public sealed class StreamService : StreamServiceGrpc
{
    private readonly StreamManager _streamManager;

    private readonly ILogger<StreamService>? _logger;

    public StreamService(StreamManager streamManager, ILogger<StreamService>? logger)
    {
        _streamManager = streamManager ?? throw new ArgumentNullException(nameof(streamManager));

        _logger = logger;
    }   

    public override async Task Call(CallRequest request, IServerStreamWriter<CallResponse> responseStream, ServerCallContext context)
    {
        var streamKey = Guid.NewGuid().ToString();
        var action = new StreamAction(responseStream, request);

        if (_streamManager.RegisterStream(streamKey, action))
        {
            try
            {
                await _streamManager.SendDataAsync(action, context.CancellationToken);

                await context.CancellationToken.AsTask();
            }
            catch (Exception exception)
            {
                _logger?.LogWarning(exception, "Error on calling stream");
            }

            _streamManager.UnregisterStream(streamKey);
        }
    }
}
