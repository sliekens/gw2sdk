using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.AstralRewards.Http;

internal sealed class PurchasedAstralRewardsRequest : IHttpRequest<HashSet<PurchasedAstralReward>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/wizardsvault/listings")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public required string? AccessToken { get; init; }

    
    public async Task<(HashSet<PurchasedAstralReward> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(
            entry => entry.GetPurchasedAstralReward()
        );
        return (value, new MessageContext(response));
    }
}
