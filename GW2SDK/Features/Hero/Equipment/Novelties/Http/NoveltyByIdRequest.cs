using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Novelties.Http;

internal sealed class NoveltyByIdRequest(int noveltyId) : IHttpRequest<Novelty>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/novelties") { AcceptEncoding = "gzip" };

    public int NoveltyId { get; } = noveltyId;

    public Language? Language { get; init; }

    
    public async Task<(Novelty Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", NoveltyId },
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
        var value = json.RootElement.GetNovelty();
        return (value, new MessageContext(response));
    }
}
