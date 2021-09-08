using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Http
{
    /// <summary>Adds missing headers to the 'Vary' list.</summary>
    [PublicAPI]
    public sealed class VaryFixHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var response = await base.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);

            if (!response.Headers.Vary.Contains("Accept-Language"))
            {
                response.Headers.Vary.Add("Accept-Language");
            }

            if (!response.Headers.Vary.Contains("X-Schema-Version"))
            {
                response.Headers.Vary.Add("X-Schema-Version");
            }

            return response;
        }
    }
}
