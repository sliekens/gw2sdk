using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Tokens.Infrastructure
{
    public sealed class JsonTokenInfoService : IJsonTokenInfoService
    {
        private readonly HttpClient _http;

        public JsonTokenInfoService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        // This is a little weird but in order for this to work, you need to add the token to HttpClient.DefaultRequestHeaders
        // Because GetStringAsync does not have an overload for headers
        // And I've looked at the sources but I didn't want to re-invent GetStringAsync with support for headers
        public async Task<string> GetTokenInfo() => await _http.GetStringAsync("/v2/tokeninfo");
    }
}
