using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Subtokens.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Subtokens
{
    [PublicAPI]
    public sealed class SubtokenService
    {
        private readonly HttpClient _http;

        public SubtokenService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<CreatedSubtoken> CreateSubtoken(
            string? accessToken,
            IReadOnlyCollection<Permission>? permissions = null,
            DateTimeOffset? absoluteExpirationDate = null,
            IReadOnlyCollection<string>? urls = null,
            JsonSerializerSettings? settings = null)
        {
            var request = new CreateSubtokenRequest(accessToken, permissions, absoluteExpirationDate, urls);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var dto = new CreatedSubtoken();
            JsonConvert.PopulateObject(json, dto, settings ?? Json.DefaultJsonSerializerSettings);
            return dto;
        }
    }
}
