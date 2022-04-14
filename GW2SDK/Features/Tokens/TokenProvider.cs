using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Tokens.Http;
using GW2SDK.Tokens.Json;
using GW2SDK.Tokens.Models;

using JetBrains.Annotations;

namespace GW2SDK.Tokens;

[PublicAPI]
public sealed class TokenProvider
{
    private readonly HttpClient http;

    public TokenProvider(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<IReplica<TokenInfo>> GetTokenInfo(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TokenInfoRequest request = new(accessToken);
        return await http.GetResource(request,
                json => TokenInfoReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
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
        CreateSubtokenRequest request = new(accessToken, permissions, absoluteExpirationDate, urls);
        return await http.GetResource(request,
                json => SubtokenReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }
}