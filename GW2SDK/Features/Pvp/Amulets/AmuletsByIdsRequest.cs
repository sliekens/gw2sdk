using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Amulets;

[PublicAPI]
public sealed class AmuletsByIdsRequest : IHttpRequest<Replica<HashSet<Amulet>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/pvp/amulets") { AcceptEncoding = "gzip" };

    public AmuletsByIdsRequest(IReadOnlyCollection<int> amuletIds)
    {
        Check.Collection(amuletIds);
        AmuletIds = amuletIds;
    }

    public IReadOnlyCollection<int> AmuletIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Amulet>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AmuletIds },
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
        return new Replica<HashSet<Amulet>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetAmulet(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
