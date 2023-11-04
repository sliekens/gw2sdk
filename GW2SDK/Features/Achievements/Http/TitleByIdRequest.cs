using GuildWars2.Achievements.Titles;
using GuildWars2.Http;

namespace GuildWars2.Achievements.Http;

internal sealed class TitleByIdRequest : IHttpRequest2<Title>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/titles")
    {
        AcceptEncoding = "gzip"
    };

    public TitleByIdRequest(int titleId)
    {
        TitleId = titleId;
    }

    public int TitleId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Title Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", TitleId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetTitle(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
