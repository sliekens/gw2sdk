using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using static System.Net.HttpStatusCode;

namespace GW2SDK.Http
{
    [PublicAPI]
    public sealed class UnauthorizedMessageHandler : DelegatingHandler
    {
        public UnauthorizedMessageHandler()
        {
        }

        public UnauthorizedMessageHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode is not (Unauthorized or Forbidden))
            {
                return response;
            }

            if (response.Content.Headers.ContentType.MediaType != "application/json")
            {
                throw new UnauthorizedOperationException("");
            }

            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            if (!json.RootElement.TryGetProperty("text", out var text))
            {
                throw new UnauthorizedOperationException("");
            }

            var reason = text.GetString();
            throw new UnauthorizedOperationException(reason);
        }
    }
}
