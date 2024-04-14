using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts.Http;

internal sealed class MountSkinsRequest : IHttpRequest<HashSet<MountSkin>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mounts/skins")
    {
        AcceptEncoding = "gzip"
    };

    public Language? Language { get; init; }

    
    public async Task<(HashSet<MountSkin> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", "all" },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(static entry => entry.GetMountSkin());
        return (value, new MessageContext(response));
    }
}
