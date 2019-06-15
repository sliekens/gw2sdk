using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Infrastructure.Common
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
                var text = JObject.Parse(json)["error"].ToString();
                throw new TooManyRequestsException(text);
            }

            return response;
        }
    }
}