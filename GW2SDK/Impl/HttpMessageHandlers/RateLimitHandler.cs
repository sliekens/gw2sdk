using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Exceptions;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Impl.HttpMessageHandlers
{
    public sealed class RateLimitHandler : DelegatingHandler
    {
        public RateLimitHandler()
        {
        }

        public RateLimitHandler([NotNull] HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCodeEx.TooManyRequests)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var text = JObject.Parse(json)["text"].ToString();
                throw new TooManyRequestsException(text);
            }

            return response;
        }
    }
}
