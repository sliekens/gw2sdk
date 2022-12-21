using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Guilds.Permissions;

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
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        return new ReplicaSet<GuildPermissionSummary>
        {
            Values = json.RootElement.GetSet(
                entry => entry.GetGuildPermissionSummary(MissingMemberBehavior)
            ),
            Context = response.Headers.GetCollectionContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
