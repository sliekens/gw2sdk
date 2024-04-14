using GuildWars2.Http;

namespace GuildWars2.Pvp.Ranks.Http;

internal sealed class RankByIdRequest(int rankId) : IHttpRequest<Rank>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/ranks") { AcceptEncoding = "gzip" };

    public int RankId { get; } = rankId;

    public Language? Language { get; init; }

    
    public async Task<(Rank Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", RankId },
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
        var value = json.RootElement.GetRank();
        return (value, new MessageContext(response));
    }
}
