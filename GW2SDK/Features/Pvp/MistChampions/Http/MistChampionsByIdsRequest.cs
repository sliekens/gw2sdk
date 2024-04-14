using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions.Http;

internal sealed class MistChampionsByIdsRequest : IHttpRequest<HashSet<MistChampion>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/heroes") { AcceptEncoding = "gzip" };

    public MistChampionsByIdsRequest(IReadOnlyCollection<string> heroIds)
    {
        Check.Collection(heroIds);
        HeroIds = heroIds;
    }

    public IReadOnlyCollection<string> HeroIds { get; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<MistChampion> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", HeroIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
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
