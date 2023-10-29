using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Stories.Http;

internal sealed class StoriesByIdsRequest : IHttpRequest<Replica<HashSet<Story>>>
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

    public async Task<Replica<HashSet<Story>>> SendAsync(
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
        return new Replica<HashSet<Story>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetStory(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
