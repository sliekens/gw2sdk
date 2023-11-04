using GuildWars2.Http;
using GuildWars2.Wvw.Upgrades;

namespace GuildWars2.Wvw.Http;

internal sealed class UpgradeByIdRequest : IHttpRequest<ObjectiveUpgrade>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/upgrades") { AcceptEncoding = "gzip" };

    public UpgradeByIdRequest(int upgradeId)
    {
        UpgradeId = upgradeId;
    }

    public int UpgradeId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(ObjectiveUpgrade Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", UpgradeId },
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
        var value = json.RootElement.GetObjectiveUpgrade(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
