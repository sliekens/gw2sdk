using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Novelties.Http;

internal sealed class NoveltiesByIdsRequest : IHttpRequest<HashSet<Novelty>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/novelties") { AcceptEncoding = "gzip" };

    public NoveltiesByIdsRequest(IReadOnlyCollection<int> noveltyIds)
    {
        Check.Collection(noveltyIds);
        NoveltyIds = noveltyIds;
    }

    public IReadOnlyCollection<int> NoveltyIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Novelty> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", NoveltyIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetNovelty(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
