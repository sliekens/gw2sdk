using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Masteries.Http;

internal sealed class MasteriesByIdsRequest : IHttpRequest2<HashSet<Mastery>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/masteries")
    {
        AcceptEncoding = "gzip"
    };

    public MasteriesByIdsRequest(IReadOnlyCollection<int> masteryIds)
    {
        MasteryIds = masteryIds;
    }

    public IReadOnlyCollection<int> MasteryIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Mastery> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MasteryIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetMastery(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
