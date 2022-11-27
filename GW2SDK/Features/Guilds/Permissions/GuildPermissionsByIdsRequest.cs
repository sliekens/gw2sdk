using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Permissions;

[PublicAPI]
public sealed class GuildPermissionsByIdsRequest : IHttpRequest<IReplicaSet<GuildPermissionSummary>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/guild/permissions") { AcceptEncoding = "gzip" };

    public GuildPermissionsByIdsRequest(IReadOnlyCollection<GuildPermission> guildPermissionIds)
    {
        Check.Collection(guildPermissionIds, nameof(guildPermissionIds));
        GuildPermissionIds = guildPermissionIds;
    }

    public IReadOnlyCollection<GuildPermission> GuildPermissionIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<GuildPermissionSummary>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments =
                    new QueryBuilder
                    {
                        { "ids", GuildPermissionIds.Select(id => id.ToString()) },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value =
            json.RootElement.GetSet(entry => entry.GetGuildPermissionSummary(MissingMemberBehavior));
        return new ReplicaSet<GuildPermissionSummary>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
