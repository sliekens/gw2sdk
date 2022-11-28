using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Tokens;

[PublicAPI]
public sealed class CreateSubtokenRequest : IHttpRequest<IReplica<CreatedSubtoken>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/createsubtoken")
    {
        AcceptEncoding = "gzip"
    };

    public CreateSubtokenRequest(string accessToken)
    {
        AccessToken = accessToken;
    }

    public string AccessToken { get; }

    public DateTimeOffset? AbsoluteExpirationDate { get; init; }

    public IReadOnlyCollection<Permission>? Permissions { get; init; }

    public IReadOnlyCollection<string>? Urls { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<CreatedSubtoken>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder args = new();
        if (Permissions is { Count: not 0 })
        {
            args.Add("permissions", string.Join(",", Permissions).ToLowerInvariant());
        }

        if (AbsoluteExpirationDate.HasValue)
        {
            args.Add("expire", AbsoluteExpirationDate.Value.ToUniversalTime().ToString("s"));
        }

        if (Urls is { Count: not 0 })
        {
            args.Add("urls", string.Join(",", Urls.Select(Uri.EscapeDataString)));
        }

        args.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = args,
                    BearerToken = AccessToken
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetCreatedSubtoken(MissingMemberBehavior);
        return new Replica<CreatedSubtoken>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
