using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;

namespace GuildWars2.Files;

[PublicAPI]
public sealed class FileByIdRequest : IHttpRequest<IReplica<File>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/files") { AcceptEncoding = "gzip" };

    public FileByIdRequest(string fileId)
    {
        FileId = fileId;
    }

    public string FileId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<File>> SendAsync(
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
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetFile(MissingMemberBehavior);
        return new Replica<File>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
