using GuildWars2.Hero.Achievements.Titles;
using GuildWars2.Http;

namespace GuildWars2.Hero.Achievements.Http;

internal sealed class TitleByIdRequest(int titleId) : IHttpRequest<Title>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/titles")
    {
        AcceptEncoding = "gzip"
    };

    public int TitleId { get; } = titleId;

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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetTitle(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
