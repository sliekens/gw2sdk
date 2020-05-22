using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Exceptions;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Impl.HttpMessageHandlers
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
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var text = JObject.Parse(json)?["text"]?.ToString();
                throw new UnauthorizedOperationException(text);
            }

            return response;
        }
    }
}
