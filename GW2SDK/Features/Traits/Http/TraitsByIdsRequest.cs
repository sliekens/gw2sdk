using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Traits.Http;

internal sealed class TraitsByIdsRequest : IHttpRequest<Replica<HashSet<Trait>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/traits")
    {
        AcceptEncoding = "gzip"
    };

    public TraitsByIdsRequest(IReadOnlyCollection<int> traitIds)
    {
        Check.Collection(traitIds);
        TraitIds = traitIds;
    }

    public IReadOnlyCollection<int> TraitIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Trait>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", TraitIds },
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
        return new Replica<HashSet<Trait>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetTrait(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
