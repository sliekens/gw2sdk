using GuildWars2.Achievements.Titles;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Http;

internal sealed class TitlesByIdsRequest : IHttpRequest<HashSet<Title>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/titles")
    {
        AcceptEncoding = "gzip"
    };

    public TitlesByIdsRequest(IReadOnlyCollection<int> titleIds)
    {
        Check.Collection(titleIds);
        TitleIds = titleIds;
    }

    public IReadOnlyCollection<int> TitleIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Title> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", TitleIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetTitle(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
