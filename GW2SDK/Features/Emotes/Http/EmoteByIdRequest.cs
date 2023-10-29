using GuildWars2.Http;

namespace GuildWars2.Emotes.Http;

internal sealed class EmoteByIdRequest : IHttpRequest<Replica<Emote>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emotes") { AcceptEncoding = "gzip" };

    public EmoteByIdRequest(string emoteId)
    {
        EmoteId = emoteId;
    }

    public string EmoteId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Emote>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", EmoteId },
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
        return new Replica<Emote>
        {
            Value = json.RootElement.GetEmote(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
