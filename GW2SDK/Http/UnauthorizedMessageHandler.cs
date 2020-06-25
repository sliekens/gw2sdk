using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Exceptions;
using Newtonsoft.Json.Linq;
using static System.Net.HttpStatusCode;

namespace GW2SDK.Http
{
    public sealed class UnauthorizedMessageHandler : DelegatingHandler
    {
        public UnauthorizedMessageHandler()
        {
        }

        public UnauthorizedMessageHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode == Unauthorized || response.StatusCode == Forbidden)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var text = JObject.Parse(json)?["text"]?.ToString();
                throw new UnauthorizedOperationException(text);
            }

            return response;
        }
    }
}
