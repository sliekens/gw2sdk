using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Http;

namespace GuildWars2.Hero.StoryJournal.Http;

internal sealed class BackstoryAnswerByIdRequest(string answerId)
    : IHttpRequest<BackgroundStoryAnswer>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/backstory/answers")
    {
        AcceptEncoding = "gzip"
    };

    public string AnswerId { get; } = answerId;

    public Language? Language { get; init; }

    
    public async Task<(BackgroundStoryAnswer Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new()
        {
            { "id", AnswerId },
            { "v", SchemaVersion.Recommended }
        };

        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = search,
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetBackgroundStoryAnswer();
        return (value, new MessageContext(response));
    }
}
