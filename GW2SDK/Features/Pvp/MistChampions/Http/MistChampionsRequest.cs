using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions.Http;

internal sealed class MistChampionsRequest : IHttpRequest<HashSet<MistChampion>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/pvp/heroes")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "ids", "all" },
            { "v", SchemaVersion.Recommended }
        }
    };

    public Language? Language { get; init; }

    
    public async Task<(HashSet<MistChampion> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { AcceptLanguage = Language?.Alpha2Code },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(static entry => entry.GetMistChampion());
        return (value, new MessageContext(response));
    }
}
