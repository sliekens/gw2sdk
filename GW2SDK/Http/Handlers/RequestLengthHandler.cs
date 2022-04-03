using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Http.Handlers;

[PublicAPI]
public sealed class RequestLengthHandler : DelegatingHandler
{
    public RequestLengthHandler()
    {
    }

    public RequestLengthHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        if (request.RequestUri?.ToString()
                .Length > 2000)
        {
            throw new RequestTooLongException("The request URI is too long.")
            {
                Data =
                {
                    ["RequestUri"] = request.RequestUri?.ToString()
                }
            };
        }

        return await base.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
    }
}
