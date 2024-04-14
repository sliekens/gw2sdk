using GuildWars2.Http;

namespace GuildWars2.Pvp.Seasons.Http;

internal sealed class SeasonByIdRequest(string seasonId) : IHttpRequest<Season>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/seasons") { AcceptEncoding = "gzip" };

    public string SeasonId { get; } = seasonId;

    public Language? Language { get; init; }

    
    public async Task<(Season Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", SeasonId },
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
        var value = json.RootElement.GetSeason();
        return (value, new MessageContext(response));
    }
}
