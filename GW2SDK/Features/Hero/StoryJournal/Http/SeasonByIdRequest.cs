using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Http;

namespace GuildWars2.Hero.StoryJournal.Http;

internal sealed class SeasonByIdRequest(string seasonId) : IHttpRequest<Storyline>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/stories/seasons") { AcceptEncoding = "gzip" };

    public string SeasonId { get; } = seasonId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Storyline Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetStoryline(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
