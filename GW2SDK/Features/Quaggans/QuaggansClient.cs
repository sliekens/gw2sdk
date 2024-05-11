using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Quaggans;

/// <summary>Provides query methods for images of Quaggans.</summary>
[PublicAPI]
public sealed class QuaggansClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="QuaggansClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public QuaggansClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves all quaggans.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Quaggan> Value, MessageContext Context)> GetQuaggans(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddAllIds();
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/quaggans", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetQuaggan());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all quaggans.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetQuaggansIndex(
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/quaggans", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a quaggan by its ID.</summary>
    /// <param name="quagganId">The quaggan ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Quaggan Value, MessageContext Context)> GetQuagganById(
        string quagganId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddId(quagganId);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/quaggans", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetQuaggan();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves quaggans by their IDs.</summary>
    /// <param name="quagganIds">The quaggan IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Quaggan> Value, MessageContext Context)> GetQuaggansByIds(
        IEnumerable<string> quagganIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddIds(quagganIds);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/quaggans", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetQuaggan());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of quaggans.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Quaggan> Value, MessageContext Context)> GetQuaggansByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddPage(pageIndex, pageSize);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/quaggans", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetQuaggan());
            return (value, response.Context);
        }
    }
}
