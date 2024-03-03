using GuildWars2.Http;

namespace GuildWars2.Items.Stats.Http;

internal sealed class AttributeCombinationByIdRequest(int attributeCombinationId) : IHttpRequest<AttributeCombination>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public int AttributeCombinationId { get; } = attributeCombinationId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(AttributeCombination Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AttributeCombinationId },
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
        var value = json.RootElement.GetAttributeCombination(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
