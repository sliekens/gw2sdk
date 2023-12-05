using GuildWars2.Http;
using GuildWars2.Pve.Home.Cats;

namespace GuildWars2.Pve.Home.Http;

internal sealed class CatByIdRequest(int catId) : IHttpRequest<Cat>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/home/cats") { AcceptEncoding = "gzip" };

    public int CatId { get; } = catId;

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
