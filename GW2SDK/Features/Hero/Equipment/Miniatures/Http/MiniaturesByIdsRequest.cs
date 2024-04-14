using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Miniatures.Http;

internal sealed class MiniaturesByIdsRequest : IHttpRequest<HashSet<Miniature>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/minis") { AcceptEncoding = "gzip" };

    public MiniaturesByIdsRequest(IReadOnlyCollection<int> miniatureIds)
    {
        Check.Collection(miniatureIds);
        MiniatureIds = miniatureIds;
    }

    public IReadOnlyCollection<int> MiniatureIds { get; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<Miniature> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MiniatureIds },
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
        var value = json.RootElement.GetSet(static entry => entry.GetMiniature());
        return (value, new MessageContext(response));
    }
}
