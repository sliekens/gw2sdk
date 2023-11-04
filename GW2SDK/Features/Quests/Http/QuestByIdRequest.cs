using GuildWars2.Http;

namespace GuildWars2.Quests.Http;

internal sealed class QuestByIdRequest : IHttpRequest<Quest>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/quests") { AcceptEncoding = "gzip" };

    public QuestByIdRequest(int questId)
    {
        QuestId = questId;
    }

    public int QuestId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Quest Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", QuestId },
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
        var value = json.RootElement.GetQuest(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
