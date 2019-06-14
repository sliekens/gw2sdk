using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Infrastructure.Common
{
    public sealed class UnauthorizedMessageHandler : DelegatingHandler
    {
        public UnauthorizedMessageHandler()
        {
        }

        public UnauthorizedMessageHandler([NotNull] HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var text = JObject.Parse(json)["text"].ToString();
                throw new UnauthorizedOperationException(text);
            }

            return response;
        }
    }
}
