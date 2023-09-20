using GuildWars2.Http;

namespace GuildWars2.Minipets;

[PublicAPI]
public sealed class MinipetByIdRequest : IHttpRequest<Replica<Minipet>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/minis") { AcceptEncoding = "gzip" };

    public MinipetByIdRequest(int minipetId)
    {
        MinipetId = minipetId;
    }

    public int MinipetId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Minipet>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MinipetId },
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
        return new Replica<Minipet>
        {
            Value = json.RootElement.GetMinipet(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
