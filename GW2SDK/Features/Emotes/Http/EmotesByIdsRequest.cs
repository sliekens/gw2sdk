using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Emotes.Http;

internal sealed class EmotesByIdsRequest : IHttpRequest<Replica<HashSet<Emote>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emotes") { AcceptEncoding = "gzip" };

    public EmotesByIdsRequest(IReadOnlyCollection<string> emoteIds)
    {
        Check.Collection(emoteIds);
        EmoteIds = emoteIds;
    }

    public IReadOnlyCollection<string> EmoteIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Emote>>> SendAsync(
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
        return new Replica<HashSet<Emote>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetEmote(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
