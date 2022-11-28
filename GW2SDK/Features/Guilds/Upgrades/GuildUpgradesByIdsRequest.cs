using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
public sealed class GuildUpgradesByIdsRequest : IHttpRequest<IReplicaSet<GuildUpgrade>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/guild/upgrades") { AcceptEncoding = "gzip" };

    public GuildUpgradesByIdsRequest(IReadOnlyCollection<int> guildUpgradeIds)
    {
        Check.Collection(guildUpgradeIds, nameof(guildUpgradeIds));
        GuildUpgradeIds = guildUpgradeIds;
    }

    public IReadOnlyCollection<int> GuildUpgradeIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<GuildUpgrade>> SendAsync(
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
                        { "ids", GuildUpgradeIds.Select(id => id.ToString()) },
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

        var value = json.RootElement.GetSet(entry => GuildUpgradeJson.GetGuildUpgrade(entry, MissingMemberBehavior));
        return new ReplicaSet<GuildUpgrade>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
