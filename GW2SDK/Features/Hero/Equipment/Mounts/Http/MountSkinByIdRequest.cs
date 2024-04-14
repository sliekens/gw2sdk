using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Mounts.Http;

internal sealed class MountSkinByIdRequest(int mountSkinId) : IHttpRequest<MountSkin>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mounts/skins")
    {
        AcceptEncoding = "gzip"
    };

    public int MountSkinId { get; } = mountSkinId;

    public Language? Language { get; init; }

    
    public async Task<(MountSkin Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MountSkinId },
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
        var value = json.RootElement.GetMountSkin();
        return (value, new MessageContext(response));
    }
}
