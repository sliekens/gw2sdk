using GuildWars2.Http;
using GuildWars2.Pvp.Seasons;

namespace GuildWars2.Pvp.Http;

internal sealed class SeasonByIdRequest : IHttpRequest<Replica<Season>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/seasons") { AcceptEncoding = "gzip" };

    public SeasonByIdRequest(string seasonId)
    {
        SeasonId = seasonId;
    }

    public string SeasonId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Season>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", SeasonId },
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
        var value = json.RootElement.GetSeason(MissingMemberBehavior);
        return new Replica<Season>
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
