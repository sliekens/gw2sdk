using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Http;

internal sealed class BackstoryQuestionsByIdsRequest : IHttpRequest<HashSet<BackgroundStoryQuestion>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/backstory/questions") { AcceptEncoding = "gzip" };

    public BackstoryQuestionsByIdsRequest(IReadOnlyCollection<int> questionIds)
    {
        Check.Collection(questionIds);
        QuestionIds = questionIds;
    }

    public IReadOnlyCollection<int> QuestionIds { get; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<BackgroundStoryQuestion> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", QuestionIds },
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
        var value =
            json.RootElement.GetSet(static entry => entry.GetBackgroundStoryQuestion());
        return (value, new MessageContext(response));
    }
}
