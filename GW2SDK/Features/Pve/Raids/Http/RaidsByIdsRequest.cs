using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pve.Raids.Http;

internal sealed class RaidsByIdsRequest : IHttpRequest<HashSet<Raid>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/raids") { AcceptEncoding = "gzip" };

    public RaidsByIdsRequest(IReadOnlyCollection<string> raidIds)
    {
        Check.Collection(raidIds);
        RaidIds = raidIds;
    }

    public IReadOnlyCollection<string> RaidIds { get; }

    
    public async Task<(HashSet<Raid> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(static entry => entry.GetRaid());
        return (value, new MessageContext(response));
    }
}
