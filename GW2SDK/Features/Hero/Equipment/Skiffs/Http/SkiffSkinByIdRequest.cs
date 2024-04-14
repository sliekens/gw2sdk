using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Skiffs.Http;

internal sealed class SkiffSkinByIdRequest(int skiffSkinId) : IHttpRequest<SkiffSkin>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/skiffs") { AcceptEncoding = "gzip" };

    public int SkiffSkinId { get; } = skiffSkinId;

    public Language? Language { get; init; }

    
    public async Task<(SkiffSkin Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", SkiffSkinId },
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
        var value = json.RootElement.GetSkiffSkin();
        return (value, new MessageContext(response));
    }
}
