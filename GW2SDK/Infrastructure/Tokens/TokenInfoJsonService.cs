using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Tokens;

namespace GW2SDK.Infrastructure.Tokens
{
    public sealed class TokenInfoJsonService : ITokenInfoJsonService
    {
        private readonly HttpClient _http;

        public TokenInfoJsonService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        // This is a little weird but in order for this to work, you need to add a token to HttpClient.DefaultRequestHeaders
        public async Task<HttpResponseMessage> GetTokenInfo()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/tokeninfo"
            };
            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }
    }
}
