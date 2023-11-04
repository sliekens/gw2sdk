using GuildWars2.Http;
using GuildWars2.Pvp.Stats;

namespace GuildWars2.Pvp.Http;

internal sealed class StatsRequest : IHttpRequest<Replica<AccountStats>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/pvp/stats")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<AccountStats>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template with { BearerToken = AccessToken }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);

        var value = json.RootElement.GetAccountStats(MissingMemberBehavior);
        return new Replica<AccountStats>
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
