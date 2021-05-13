using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Subtokens.Http;

namespace GW2SDK.Subtokens
{
    [PublicAPI]
    public sealed class SubtokenService
    {
        private readonly HttpClient _http;

        private readonly ISubtokenReader _subtokenReader;

        public SubtokenService(HttpClient http, ISubtokenReader subtokenReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _subtokenReader = subtokenReader ?? throw new ArgumentNullException(nameof(subtokenReader));
        }

        public async Task<CreatedSubtoken> CreateSubtoken(
            string? accessToken,
            IReadOnlyCollection<Permission>? permissions = null,
            DateTimeOffset? absoluteExpirationDate = null,
            IReadOnlyCollection<string>? urls = null
        )
        {
            var request = new CreateSubtokenRequest(accessToken, permissions, absoluteExpirationDate, urls);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _subtokenReader.Read(json);
        }
    }
}
