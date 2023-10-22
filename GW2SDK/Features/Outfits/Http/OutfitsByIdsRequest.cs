using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Outfits.Http;

internal sealed class OutfitsByIdsRequest : IHttpRequest<Replica<HashSet<Outfit>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/outfits") { AcceptEncoding = "gzip" };

    public OutfitsByIdsRequest(IReadOnlyCollection<int> outfitIds)
    {
        Check.Collection(outfitIds);
        OutfitIds = outfitIds;
    }

    public IReadOnlyCollection<int> OutfitIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Outfit>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", OutfitIds },
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
        return new Replica<HashSet<Outfit>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetOutfit(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
