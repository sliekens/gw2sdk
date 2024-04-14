using GuildWars2.Guilds.Emblems;
using GuildWars2.Http;

namespace GuildWars2.Guilds.Http;

internal sealed class EmblemBackgroundByIdRequest(int backgroundEmblemId)
    : IHttpRequest<EmblemBackground>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emblem/backgrounds") { AcceptEncoding = "gzip" };

    public int BackgroundEmblemId { get; } = backgroundEmblemId;

    
    public async Task<(EmblemBackground Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", BackgroundEmblemId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetEmblemBackground();
        return (value, new MessageContext(response));
    }
}
