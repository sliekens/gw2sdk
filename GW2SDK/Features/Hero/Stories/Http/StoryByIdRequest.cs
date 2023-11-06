using GuildWars2.Http;

namespace GuildWars2.Hero.Stories.Http;

internal sealed class StoryByIdRequest : IHttpRequest<Story>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/stories") { AcceptEncoding = "gzip" };

    public StoryByIdRequest(int storyId)
    {
        StoryId = storyId;
    }

    public int StoryId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Story Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", StoryId },
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
        var value = json.RootElement.GetStory(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
