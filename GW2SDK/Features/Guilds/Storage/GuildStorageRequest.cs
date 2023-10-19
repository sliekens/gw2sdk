using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Storage;

[PublicAPI]
public sealed class GuildStorageRequest : IHttpRequest<Replica<List<GuildStorageSlot>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/:id/storage") { AcceptEncoding = "gzip" };

    public GuildStorageRequest(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<List<GuildStorageSlot>>> SendAsync(
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
        return new Replica<List<GuildStorageSlot>>
        {
            Value =
                json.RootElement.GetList(
                    entry => entry.GetGuildStorageSlot(MissingMemberBehavior)
                ),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
