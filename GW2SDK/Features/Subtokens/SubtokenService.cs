using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Subtokens;
using Newtonsoft.Json;

namespace GW2SDK.Features.Subtokens
{
    [PublicAPI]
    public sealed class SubtokenService
    {
        private readonly HttpClient _http;

        public SubtokenService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<CreatedSubtoken> CreateSubtoken(
            [CanBeNull] string accessToken,
            [CanBeNull] IReadOnlyList<Permission> permissions = null,
            DateTimeOffset? absoluteExpirationDate = null,
            [CanBeNull] IReadOnlyList<string> urls = null,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new CreateSubtokenRequest.Builder(accessToken, permissions, absoluteExpirationDate, urls).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var dto = new CreatedSubtoken();
                JsonConvert.PopulateObject(json, dto, settings ?? Json.DefaultJsonSerializerSettings);
                return dto;
            }
        }
    }
}
