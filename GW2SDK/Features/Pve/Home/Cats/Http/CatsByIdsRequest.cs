using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Cats.Http;

internal sealed class CatsByIdsRequest : IHttpRequest<HashSet<Cat>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/home/cats") { AcceptEncoding = "gzip" };

    public CatsByIdsRequest(IReadOnlyCollection<int> catIds)
    {
        Check.Collection(catIds);
        CatIds = catIds;
    }

    public IReadOnlyCollection<int> CatIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Cat> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", CatIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetCat(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
