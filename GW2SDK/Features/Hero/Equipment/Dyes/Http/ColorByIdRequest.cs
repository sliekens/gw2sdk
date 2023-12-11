using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Dyes.Http;

internal sealed class ColorByIdRequest(int colorId) : IHttpRequest<Dye>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/colors")
    {
        AcceptEncoding = "gzip"
    };

    public int ColorId { get; } = colorId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Dye Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", ColorId },
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
        var value = json.RootElement.GetDye(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
