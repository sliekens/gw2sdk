using GuildWars2.Home.Cats;
using GuildWars2.Http;

namespace GuildWars2.Home.Http;

internal sealed class CatByIdRequest : IHttpRequest<Cat>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/home/cats") { AcceptEncoding = "gzip" };

    public CatByIdRequest(int catId)
    {
        CatId = catId;
    }

    public int CatId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Cat Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetCat(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
