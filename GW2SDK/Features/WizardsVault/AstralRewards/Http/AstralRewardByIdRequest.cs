using GuildWars2.Http;

namespace GuildWars2.WizardsVault.AstralRewards.Http;

internal sealed class AstralRewardByIdRequest(int astralRewardId) : IHttpRequest<AstralReward>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wizardsvault/listings") { AcceptEncoding = "gzip" };

    public int AstralRewardId { get; } = astralRewardId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(AstralReward Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AstralRewardId },
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
        var value = json.RootElement.GetAstralReward(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
