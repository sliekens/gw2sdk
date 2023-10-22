using GuildWars2.Home.Cats;
using GuildWars2.Http;

namespace GuildWars2.Home.Http;

internal sealed class CatByIdRequest : IHttpRequest<Replica<Cat>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/home/cats") { AcceptEncoding = "gzip" };

    public CatByIdRequest(int catId)
    {
        CatId = catId;
    }

    public int CatId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Cat>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", CatId },
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
        return new Replica<Cat>
        {
            Value = json.RootElement.GetCat(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
