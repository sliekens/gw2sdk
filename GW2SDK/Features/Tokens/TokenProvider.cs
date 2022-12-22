using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GuildWars2.Tokens;

[PublicAPI]
public sealed class TokenProvider
{
    private readonly HttpClient http;

    public TokenProvider(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<Replica<TokenInfo>> GetTokenInfo(
        string accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TokenInfoRequest request = new(accessToken)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<CreatedSubtoken>> CreateSubtoken(
        string accessToken,
        IReadOnlyCollection<Permission>? permissions = null,
        DateTimeOffset? absoluteExpirationDate = null,
        IReadOnlyCollection<string>? urls = null,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CreateSubtokenRequest request = new(accessToken)
        {
            Permissions = permissions,
            AbsoluteExpirationDate = absoluteExpirationDate,
            Urls = urls,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
