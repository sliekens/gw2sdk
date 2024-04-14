using GuildWars2.Http;

namespace GuildWars2.Files.Http;

internal sealed class FileByIdRequest(string fileId) : IHttpRequest<Asset>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/files") { AcceptEncoding = "gzip" };

    public string FileId { get; } = fileId;

    
    public async Task<(Asset Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", FileId },
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
        var value = json.RootElement.GetAsset();
        return (value, new MessageContext(response));
    }
}
