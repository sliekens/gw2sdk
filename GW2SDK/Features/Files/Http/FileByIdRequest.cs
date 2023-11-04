using GuildWars2.Http;

namespace GuildWars2.Files.Http;

internal sealed class FileByIdRequest : IHttpRequest2<Asset>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/files") { AcceptEncoding = "gzip" };

    public FileByIdRequest(string fileId)
    {
        FileId = fileId;
    }

    public string FileId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetAsset(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
