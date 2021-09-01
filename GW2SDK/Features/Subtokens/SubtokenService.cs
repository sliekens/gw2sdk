using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Subtokens.Http;
using JetBrains.Annotations;

namespace GW2SDK.Subtokens
{
    [PublicAPI]
    public sealed class SubtokenService
    {
        private readonly HttpClient http;

        private readonly ISubtokenReader subtokenReader;
        private readonly MissingMemberBehavior missingMemberBehavior;

        public SubtokenService(HttpClient http, ISubtokenReader subtokenReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.subtokenReader = subtokenReader ?? throw new ArgumentNullException(nameof(subtokenReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<CreatedSubtoken>> CreateSubtoken(
            string? accessToken,
            IReadOnlyCollection<Permission>? permissions = null,
            DateTimeOffset? absoluteExpirationDate = null,
            IReadOnlyCollection<string>? urls = null,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CreateSubtokenRequest(accessToken, permissions, absoluteExpirationDate, urls);
            return await http.GetResource(request, json => subtokenReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
