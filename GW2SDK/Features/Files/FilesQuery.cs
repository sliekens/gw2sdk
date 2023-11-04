using GuildWars2.Files.Http;

namespace GuildWars2.Files;

[PublicAPI]
public sealed class FilesQuery
{
    private readonly HttpClient http;

    public FilesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<Asset> Value, MessageContext Context)> GetFiles(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FilesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetFilesIndex(
        CancellationToken cancellationToken = default
    )
    {
        FilesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Asset Value, MessageContext Context)> GetFileById(
        string fileId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FileByIdRequest request = new(fileId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Asset> Value, MessageContext Context)> GetFilesByIds(
        IReadOnlyCollection<string> fileIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FilesByIdsRequest request = new(fileIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Asset> Value, MessageContext Context)> GetFilesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FilesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
