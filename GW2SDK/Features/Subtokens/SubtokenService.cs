using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Subtokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Features.Subtokens
{
    public sealed class SubtokenService
    {
        private readonly HttpClient _http;

        public SubtokenService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<CreatedSubtoken> CreateSubtoken(
            [CanBeNull] string accessToken = null,
            [CanBeNull] IReadOnlyList<Permission> permissions = null,
            DateTimeOffset? absoluteExpirationDate = null,
            [CanBeNull] IReadOnlyList<string> urls = null,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new CreateSubtokenRequest.Builder(accessToken, permissions, absoluteExpirationDate, urls).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    var text = JObject.Parse(json)["text"].ToString();
                    throw new UnauthorizedOperationException(text);
                }

                response.EnsureSuccessStatusCode();
                var dto = new CreatedSubtoken();
                JsonConvert.PopulateObject(json, dto, settings ?? Json.DefaultJsonSerializerSettings);
                return dto;
            }
        }
    }
}
