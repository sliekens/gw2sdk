using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using static System.Net.HttpStatusCode;

namespace GW2SDK.Http
{
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
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode is BadRequest)
            {
                if (response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
                    if (json.RootElement.TryGetProperty("text", out var text))
                    {
                        var reason = text.GetString();
                        if (reason == "ErrTimeout")
                        {
                            // Sometimes the API responds with 400 Bad Request and message ErrTimeout
                            // That's not a user error and should be handled as 503 Service Unavailable
                            throw new TimeoutException();
                        }

                        throw new ArgumentException(reason);
                    }
                }

                throw new ArgumentException("");
            }

            return response;
        }
    }
}
