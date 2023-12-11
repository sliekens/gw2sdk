using GuildWars2.Http;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildByIdRequest(string id) : IHttpRequest<Guild>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/:id") { AcceptEncoding = "gzip" };

    public string Id { get; } = id;

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Guild Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", Id),
                    Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetGuild(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
