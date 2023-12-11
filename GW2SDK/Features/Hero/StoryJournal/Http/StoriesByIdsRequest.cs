using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Http;

internal sealed class StoriesByIdsRequest : IHttpRequest<HashSet<Story>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/stories") { AcceptEncoding = "gzip" };

    public StoriesByIdsRequest(IReadOnlyCollection<int> storyIds)
    {
        Check.Collection(storyIds);
        StoryIds = storyIds;
    }

    public IReadOnlyCollection<int> StoryIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Story> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", StoryIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetStory(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
