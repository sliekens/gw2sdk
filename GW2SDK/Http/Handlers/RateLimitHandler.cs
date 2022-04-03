using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using static GW2SDK.Http.HttpStatusCodeEx;

namespace GW2SDK.Http.Handlers;

[PublicAPI]
public sealed class RateLimitHandler : DelegatingHandler
{
    public RateLimitHandler()
    {
    }

    public RateLimitHandler(HttpMessageHandler innerHandler)
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
        if (response.StatusCode is TooManyRequests)
        {
            if (response.Content.Headers.ContentType?.MediaType != "application/json")
            {
                throw new TooManyRequestsException(response.ReasonPhrase)
                {
                    Data =
                    {
                        ["RequestUri"] = request.RequestUri?.ToString()
                    }
                };
            }

            using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
                .ConfigureAwait(false);
            if (!json.RootElement.TryGetProperty("text", out var text))
            {
                throw new TooManyRequestsException(response.ReasonPhrase)
                {
                    Data =
                    {
                        ["RequestUri"] = request.RequestUri?.ToString()
                    }
                };
            }

            throw new TooManyRequestsException(text.GetString())
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
