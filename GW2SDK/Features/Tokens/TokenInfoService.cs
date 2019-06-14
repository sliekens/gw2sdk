﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Tokens;
using Newtonsoft.Json;

namespace GW2SDK.Features.Tokens
{
    public sealed class TokenInfoService
    {
        private readonly HttpClient _http;

        public TokenInfoService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<TokenInfo> GetTokenInfo([CanBeNull] string accessToken, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetTokenInfoRequest.Builder(accessToken).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<TokenInfo>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }
    }
}
