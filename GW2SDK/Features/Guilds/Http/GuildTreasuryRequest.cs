using GuildWars2.Guilds.Treasury;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildTreasuryRequest : IHttpRequest<Replica<List<GuildTreasurySlot>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/:id/treasury") { AcceptEncoding = "gzip" };

    public GuildTreasuryRequest(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public required string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<List<GuildTreasurySlot>>> SendAsync(
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
        return new Replica<List<GuildTreasurySlot>>
        {
            Value =
                json.RootElement.GetList(
                    entry => entry.GetGuildTreasurySlot(MissingMemberBehavior)
                ),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
