using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Wardrobe.Http;

internal sealed class SkinByIdRequest : IHttpRequest<Skin>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/skins")
    {
        AcceptEncoding = "gzip"
    };

    public SkinByIdRequest(int skinId)
    {
        SkinId = skinId;
    }

    public int SkinId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Skin Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", SkinId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSkin(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
