using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Http.Handlers;

[PublicAPI]
public sealed class GatewayErrorHandler : DelegatingHandler
{
    public GatewayErrorHandler()
    {
    }

    public GatewayErrorHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var response = await base.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
        if ((int) response.StatusCode >= 500)
        {
            throw new GatewayException(response.StatusCode, response.ReasonPhrase)
            {
                Data =
                {
                    ["RequestUri"] = request.RequestUri?.ToString()
                }
            };
        }

        return response;
    }
}
