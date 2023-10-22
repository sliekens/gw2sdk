using GuildWars2.Http;

namespace GuildWars2.Novelties.Http;

[PublicAPI]
public sealed class NoveltyByIdRequest : IHttpRequest<Replica<Novelty>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/novelties") { AcceptEncoding = "gzip" };

    public NoveltyByIdRequest(int noveltyId)
    {
        NoveltyId = noveltyId;
    }

    public int NoveltyId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Novelty>> SendAsync(
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
        return new Replica<Novelty>
        {
            Value = json.RootElement.GetNovelty(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
