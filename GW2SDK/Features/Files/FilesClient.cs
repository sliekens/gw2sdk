using GuildWars2.Http;
using GuildWars2.Json;

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
    public async Task<(HashSet<Asset> Value, MessageContext Context)> GetFiles(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddAllIds();
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/files", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetAsset());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all the available files.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetFilesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/files", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a file by its ID.</summary>
    /// <param name="fileId">The file ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Asset Value, MessageContext Context)> GetFileById(
        string fileId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddId(fileId);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/files", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetAsset();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves files by their IDs.</summary>
    /// <param name="fileIds">The file IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Asset> Value, MessageContext Context)> GetFilesByIds(
        IEnumerable<string> fileIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddIds(fileIds);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/files", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetAsset());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of files.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Asset> Value, MessageContext Context)> GetFilesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddPage(pageIndex, pageSize);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/files", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetAsset());
            return (value, response.Context);
        }
    }
}
