using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Http;

internal sealed class QuestsByIdsRequest : IHttpRequest<HashSet<Quest>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/quests") { AcceptEncoding = "gzip" };

    public QuestsByIdsRequest(IReadOnlyCollection<int> questIds)
    {
        Check.Collection(questIds);
        QuestIds = questIds;
    }

    public IReadOnlyCollection<int> QuestIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Quest> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", QuestIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetQuest(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
