using EQUTech.Core.Grpc.Services.JokerBoard.Stream;
using Grpc.Core;

namespace EQUTech.JokerBoard.Web.Stream;

public class StreamAction
{
    public StreamAction(IServerStreamWriter<CallResponse> responseStream, CallRequest request)
    {
        ResponseStream = responseStream;

        Request = request;
    }

    public IServerStreamWriter<CallResponse> ResponseStream { get; }

    public CallRequest Request { get; set; }
}