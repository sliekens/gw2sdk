using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Ranks;

namespace GuildWars2.Wvw.Http;

internal sealed class RanksRequest : IHttpRequest2<HashSet<Rank>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/wvw/ranks")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "ids", "all" },
            { "v", SchemaVersion.Recommended }
        }
    };

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Rank> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template with { AcceptLanguage = Language?.Alpha2Code }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetRank(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
