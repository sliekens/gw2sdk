using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.AstralRewards.Http;

internal sealed class AstralRewardsByIdsRequest : IHttpRequest<HashSet<AstralReward>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wizardsvault/listings") { AcceptEncoding = "gzip" };

    public AstralRewardsByIdsRequest(IReadOnlyCollection<int> astralRewardIds)
    {
        Check.Collection(astralRewardIds);
        AstralRewardIds = astralRewardIds;
    }

    public IReadOnlyCollection<int> AstralRewardIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<AstralReward> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AstralRewardIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetAstralReward(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
