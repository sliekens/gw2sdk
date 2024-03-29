using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts.Http;

internal sealed class UnlockedMountsRequest : IHttpRequest<HashSet<Extensible<MountName>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/mounts/types")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public required string? AccessToken { get; init; }

    public async Task<(HashSet<Extensible<MountName>> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetMountName());
        return (value, new MessageContext(response));
    }
}
