using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Subtokens.Http;
using JetBrains.Annotations;

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
            return await _http.GetResource(request, json => _subtokenReader.Read(json))
                .ConfigureAwait(false);
        }
    }
}
