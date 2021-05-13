using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using static GW2SDK.Http.HttpStatusCodeEx;

namespace GW2SDK.Http
{
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
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode is not TooManyRequests)
            {
                return response;
            }

            if (response.Content.Headers.ContentType?.MediaType != "application/json")
            {
                throw new TooManyRequestsException("");
            }

            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            if (!json.RootElement.TryGetProperty("text", out var text))
            {
                throw new TooManyRequestsException("");
            }

            var reason = text.GetString();
            throw new TooManyRequestsException(reason);
        }
    }
}
