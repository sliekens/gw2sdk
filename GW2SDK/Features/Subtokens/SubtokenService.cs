using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Subtokens.Http;
using GW2SDK.Subtokens.Json;
using JetBrains.Annotations;

namespace GW2SDK.Subtokens
{
    [PublicAPI]
    public sealed class SubtokenService
    {
        private readonly HttpClient http;

        public SubtokenService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplica<CreatedSubtoken>> CreateSubtoken(
            string? accessToken,
            IReadOnlyCollection<Permission>? permissions = null,
            DateTimeOffset? absoluteExpirationDate = null,
            IReadOnlyCollection<string>? urls = null,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CreateSubtokenRequest(accessToken, permissions, absoluteExpirationDate, urls);
            return await http.GetResource(request,
                    json => SubtokenReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
