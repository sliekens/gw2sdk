using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats.Http;

internal sealed class AttributeCombinationsByIdsRequest : IHttpRequest<HashSet<AttributeCombination>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public AttributeCombinationsByIdsRequest(IReadOnlyCollection<int> attributeCombinationIds)
    {
        Check.Collection(attributeCombinationIds);
        AttributeCombinationIds = attributeCombinationIds;
    }

    public IReadOnlyCollection<int> AttributeCombinationIds { get; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<AttributeCombination> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AttributeCombinationIds },
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
        var value = json.RootElement.GetSet(static entry => entry.GetAttributeCombination());
        return (value, new MessageContext(response));
    }
}
