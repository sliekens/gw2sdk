using GuildWars2.Guilds.Ranks;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildRanksRequest : IHttpRequest<List<GuildRank>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/:id/ranks") { AcceptEncoding = "gzip" };

    public GuildRanksRequest(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(List<GuildRank> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetList(entry => entry.GetGuildRank(MissingMemberBehavior)).OrderBy(rank => rank.Order).ToList();
        return (value, new MessageContext(response));
    }
}
