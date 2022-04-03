using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using static System.Net.HttpStatusCode;

namespace GW2SDK.Http.Handlers;

[PublicAPI]
public sealed class BadMessageHandler : DelegatingHandler
{
    public BadMessageHandler()
    {
    }

    public BadMessageHandler(HttpMessageHandler innerHandler)
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
        if (response.StatusCode is BadRequest)
        {
            if (response.Content.Headers.ContentType?.MediaType != "application/json")
            {
                throw new ArgumentException(response.ReasonPhrase)
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
                throw new ArgumentException(response.ReasonPhrase)
                {
                    Data =
                    {
                        ["RequestUri"] = request.RequestUri?.ToString()
                    }
                };
            }

            var reason = text.GetString();
            if (reason == "ErrTimeout")
            {
                // Sometimes the API responds with 400 Bad Request and message ErrTimeout
                // That's not a user error and should be handled as 503 Service Unavailable
                throw new TimeoutException
                {
                    Data =
                    {
                        ["RequestUri"] = request.RequestUri?.ToString()
                    }
                };
            }

            throw new ArgumentException(reason)
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
