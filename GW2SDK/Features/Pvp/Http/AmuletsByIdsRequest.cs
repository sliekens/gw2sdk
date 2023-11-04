using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Pvp.Amulets;

namespace GuildWars2.Pvp.Http;

internal sealed class AmuletsByIdsRequest : IHttpRequest2<HashSet<Amulet>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/amulets") { AcceptEncoding = "gzip" };

    public AmuletsByIdsRequest(IReadOnlyCollection<int> amuletIds)
    {
        Check.Collection(amuletIds);
        AmuletIds = amuletIds;
    }

    public IReadOnlyCollection<int> AmuletIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Amulet> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AmuletIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetAmulet(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
