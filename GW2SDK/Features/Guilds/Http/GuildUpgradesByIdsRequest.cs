using GuildWars2.Guilds.Upgrades;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildUpgradesByIdsRequest : IHttpRequest<HashSet<GuildUpgrade>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/upgrades") { AcceptEncoding = "gzip" };

    public GuildUpgradesByIdsRequest(IReadOnlyCollection<int> guildUpgradeIds)
    {
        Check.Collection(guildUpgradeIds);
        GuildUpgradeIds = guildUpgradeIds;
    }

    public IReadOnlyCollection<int> GuildUpgradeIds { get; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<GuildUpgrade> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", GuildUpgradeIds },
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
        var value = json.RootElement.GetSet(static entry => entry.GetGuildUpgrade());
        return (value, new MessageContext(response));
    }
}
