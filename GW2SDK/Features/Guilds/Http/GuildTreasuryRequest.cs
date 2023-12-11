using GuildWars2.Guilds.Treasury;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildTreasuryRequest(string id) : IHttpRequest<List<GuildTreasurySlot>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/:id/treasury") { AcceptEncoding = "gzip" };

    public string Id { get; } = id;

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(List<GuildTreasurySlot> Value, MessageContext Context)> SendAsync(
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
        var value =
            json.RootElement.GetList(entry => entry.GetGuildTreasurySlot(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
