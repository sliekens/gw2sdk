using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Permissions;

[PublicAPI]
public sealed class GuildPermissionsRequest : IHttpRequest<IReplicaSet<GuildPermissionSummary>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "/v2/guild/permissions")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "ids", "all" }, { "v", SchemaVersion.Recommended } }
        };

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<GuildPermissionSummary>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { AcceptLanguage = Language?.Alpha2Code },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value =
            json.RootElement.GetSet(entry => entry.GetGuildPermission(MissingMemberBehavior));
        return new ReplicaSet<GuildPermissionSummary>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
