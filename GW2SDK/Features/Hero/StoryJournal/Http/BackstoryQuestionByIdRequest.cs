using GuildWars2.Hero.StoryJournal.Backstory;
using GuildWars2.Http;

namespace GuildWars2.Hero.StoryJournal.Http;

internal sealed class BackstoryQuestionByIdRequest(int questionId) : IHttpRequest<BackstoryQuestion>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/backstory/questions") { AcceptEncoding = "gzip" };

    public int QuestionId { get; } = questionId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(BackstoryQuestion Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", QuestionId },
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
        var value = json.RootElement.GetBackstoryQuestion(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
