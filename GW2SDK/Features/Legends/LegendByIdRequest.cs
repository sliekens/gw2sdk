using GuildWars2.Http;

namespace GuildWars2.Legends;

[PublicAPI]
public sealed class LegendByIdRequest : IHttpRequest<Replica<Legend>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/legends") { AcceptEncoding = "gzip" };

    public LegendByIdRequest(string legendId)
    {
        LegendId = legendId;
    }

    public string LegendId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Legend>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", LegendId },
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
        return new Replica<Legend>
        {
            Value = json.RootElement.GetLegend(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
