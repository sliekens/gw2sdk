using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Http
{
    public sealed class BadMessageHandler : DelegatingHandler
    {
        public BadMessageHandler()
        {
        }

        public BadMessageHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var text = JObject.Parse(json)?["text"]?.ToString();
                throw new ArgumentException(text);
            }

            return response;
        }
    }
}
