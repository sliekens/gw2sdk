using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Emotes.Http;

internal sealed class EmotesByIdsRequest : IHttpRequest<HashSet<Emote>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emotes") { AcceptEncoding = "gzip" };

    public EmotesByIdsRequest(IReadOnlyCollection<string> emoteIds)
    {
        Check.Collection(emoteIds);
        EmoteIds = emoteIds;
    }

    public IReadOnlyCollection<string> EmoteIds { get; }

    
    public async Task<(HashSet<Emote> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", EmoteIds },
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
        var value = json.RootElement.GetSet(static entry => entry.GetEmote());
        return (value, new MessageContext(response));
    }
}
