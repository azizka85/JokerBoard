using EQUTech.Core.Grpc.Services.JokerBoard.Stream;
using EQUTech.Core.Services.JokerBoard;
using Grpc.Core;
using System.Collections.Concurrent;

namespace EQUTech.JokerBoard.Web.Stream;

public sealed class StreamManager
{
    private readonly ICategoryItemService _categoryItemService;

    public StreamManager(ICategoryItemService categoryItemService)
    {
        _categoryItemService = categoryItemService ?? throw new ArgumentNullException(nameof(categoryItemService));

        Streams = new ConcurrentDictionary<string, StreamAction>();
    }

    public ConcurrentDictionary<string, StreamAction> Streams { get; }

    public bool RegisterStream(string streamKey, StreamAction action)
    {
        return Streams.TryAdd(streamKey, action);
    }

    public bool UnregisterStream(string streamKey)
    {
        return Streams.TryRemove(streamKey, out _);
    }

    public async Task SendAsync(IEnumerable<IServerStreamWriter<CallResponse>> streams, CallResponse response, CancellationToken cancellationToken)
    {
        foreach(var stream in streams)
        {
            await stream.WriteAsync(response, cancellationToken);
        }
    }

    public async Task SendDataAsync(StreamAction action, CancellationToken cancellationToken)
    {
        var list = _categoryItemService.List();

        var response = new CallResponse();

        response.CategoryItems = list;

        await action.ResponseStream.WriteAsync(response, cancellationToken);
    }
}
