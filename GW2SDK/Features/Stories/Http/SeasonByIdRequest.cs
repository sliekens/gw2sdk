using GuildWars2.Http;

namespace GuildWars2.Stories.Http;

internal sealed class SeasonByIdRequest : IHttpRequest2<Season>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/stories/seasons") { AcceptEncoding = "gzip" };

    public SeasonByIdRequest(string seasonId)
    {
        SeasonId = seasonId;
    }

    public string SeasonId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSeason(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
