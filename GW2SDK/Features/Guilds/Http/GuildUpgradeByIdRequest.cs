using GuildWars2.Guilds.Upgrades;
using GuildWars2.Http;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildUpgradeByIdRequest : IHttpRequest<GuildUpgrade>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/upgrades") { AcceptEncoding = "gzip" };

    public GuildUpgradeByIdRequest(int guildUpgradeId)
    {
        GuildUpgradeId = guildUpgradeId;
    }

    public int GuildUpgradeId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(GuildUpgrade Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", GuildUpgradeId.ToString() },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetGuildUpgrade(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
