﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
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
        public async Task<(string Json, Dictionary<string, string> MetaData)> GetTokenInfo()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/tokeninfo"
            };
            return await _http.GetStringWithMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }
    }
}
