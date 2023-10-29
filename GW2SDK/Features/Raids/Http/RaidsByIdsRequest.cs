using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Raids.Http;

internal sealed class RaidsByIdsRequest : IHttpRequest<Replica<HashSet<Raid>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/raids") { AcceptEncoding = "gzip" };

    public RaidsByIdsRequest(IReadOnlyCollection<string> raidIds)
    {
        Check.Collection(raidIds);
        RaidIds = raidIds;
    }

    public IReadOnlyCollection<string> RaidIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Raid>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", RaidIds },
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
        return new Replica<HashSet<Raid>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetRaid(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
