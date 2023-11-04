using GuildWars2.Http;
using GuildWars2.Wvw.Ranks;

namespace GuildWars2.Wvw.Http;

internal sealed class RankByIdRequest : IHttpRequest<Replica<Rank>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/ranks") { AcceptEncoding = "gzip" };

    public RankByIdRequest(int rankId)
    {
        RankId = rankId;
    }

    public int RankId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Rank>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", RankId },
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
        var value = json.RootElement.GetRank(MissingMemberBehavior);
        return new Replica<Rank>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
