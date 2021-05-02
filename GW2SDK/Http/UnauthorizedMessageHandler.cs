using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
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
            if (response.StatusCode is Unauthorized or Forbidden)
            {
                if (response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
                    if (json.RootElement.TryGetProperty("text", out var text))
                    {
                        var reason = text.GetString();
                        throw new UnauthorizedOperationException(reason);
                    }
                }

                throw new UnauthorizedOperationException("");
            }

            return response;
        }
    }
}
