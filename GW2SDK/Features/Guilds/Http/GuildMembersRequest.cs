using GuildWars2.Guilds.Members;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildMembersRequest : IHttpRequest<List<GuildMember>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/:id/members") { AcceptEncoding = "gzip" };

    public GuildMembersRequest(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(List<GuildMember> Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetList(entry => entry.GetGuildMember(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
