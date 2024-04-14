using GuildWars2.Guilds.Storage;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildStorageRequest(string id) : IHttpRequest<List<GuildStorageSlot>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/:id/storage") { AcceptEncoding = "gzip" };

    public string Id { get; } = id;

    public required string? AccessToken { get; init; }

    
    public async Task<(List<GuildStorageSlot> Value, MessageContext Context)> SendAsync(
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
            json.RootElement.GetList(static entry => entry.GetGuildStorageSlot());
        return (value, new MessageContext(response));
    }
}
