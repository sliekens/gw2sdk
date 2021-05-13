using System;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Http;
using GW2SDK.Tokens.Http;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    public sealed class TokenInfoService
    {
        private readonly HttpClient _http;

        private readonly ITokenInfoReader _tokenInfoReader;

        public TokenInfoService(HttpClient http, ITokenInfoReader tokenInfoReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _tokenInfoReader = tokenInfoReader ?? throw new ArgumentNullException(nameof(tokenInfoReader));
        }

        public async Task<TokenInfo?> GetTokenInfo(string? accessToken)
        {
            var request = new TokenInfoRequest(accessToken);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _tokenInfoReader.Read(json);
        }
    }
}
