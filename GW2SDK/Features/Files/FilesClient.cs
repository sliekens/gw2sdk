using GuildWars2.Files.Http;

namespace GuildWars2.Files;

/// <summary>Provides query methods to retrieve assets (icons) from the Guild Wars 2 API.</summary>
[PublicAPI]
public sealed class FilesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="FilesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public FilesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves all the available files.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Asset> Value, MessageContext Context)> GetFiles(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FilesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all the available files.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetFilesIndex(
        CancellationToken cancellationToken = default
    )
    {
        FilesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a file by its ID.</summary>
    /// <param name="fileId">The file ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Asset Value, MessageContext Context)> GetFileById(
        string fileId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FileByIdRequest request = new(fileId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves files by their IDs.</summary>
    /// <param name="fileIds">The file IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Asset> Value, MessageContext Context)> GetFilesByIds(
        IEnumerable<string> fileIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FilesByIdsRequest request = new(fileIds.ToList()) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of files.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Asset> Value, MessageContext Context)> GetFilesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FilesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
