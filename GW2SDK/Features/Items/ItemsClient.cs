using System.Runtime.CompilerServices;
using GuildWars2.Http;
using GuildWars2.Items.Stats;
using GuildWars2.Json;

namespace GuildWars2.Items;

/// <summary>Provides query methods for items and item stats.</summary>
[PublicAPI]
public sealed class ItemsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="ItemsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public ItemsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/items

    /// <summary>Retrieves the IDs of all items.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetItemsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/items", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves an item by its ID.</summary>
    /// <param name="itemId">The item ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Item Value, MessageContext Context)> GetItemById(
        int itemId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddId(itemId);
        query.AddLanguage(language);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/items", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetItem();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves items by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="itemIds">The item IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Item> Value, MessageContext Context)> GetItemsByIds(
        IEnumerable<int> itemIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddIds(itemIds);
        query.AddLanguage(language);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/items", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetItem());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of items.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Item> Value, MessageContext Context)> GetItemsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddPage(pageIndex, pageSize);
        query.AddLanguage(language);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/items", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetItem());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves items by their IDs by chunking requests and executing them in parallel. Supports more than 200 IDs.</summary>
    /// <param name="itemIds">The item IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public IAsyncEnumerable<(Item Value, MessageContext Context)> GetItemsBulk(
        IEnumerable<int> itemIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            itemIds,
            GetChunk,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );

        // ReSharper disable once VariableHidesOuterVariable (intended, believe it or not)
        async Task<IReadOnlyCollection<(Item, MessageContext)>> GetChunk(
            IEnumerable<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var (values, context) = await GetItemsByIds(
                    chunk,
                    language,
                    missingMemberBehavior,
                    cancellationToken
                )
                .ConfigureAwait(false);
            return values.Select(value => (item: value, context)).ToList();
        }
    }

    /// <summary>Retrieves all items by chunking requests and executing them in parallel.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public async IAsyncEnumerable<(Item Value, MessageContext Context)> GetItemsBulk(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var (value, _) = await GetItemsIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetItemsBulk(
            value,
            language,
            missingMemberBehavior,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach (var item in producer.ConfigureAwait(false))
        {
            yield return item;
        }
    }

    #endregion v2/items

    #region v2/itemstats

    /// <summary>Retrieves the IDs of all attribute combinations.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetAttributeCombinationsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var query = new QueryBuilder();
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/itemstats", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves all attribute combinations.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<AttributeCombination> Value, MessageContext Context)>
        GetAttributeCombinations(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var query = new QueryBuilder();
        query.AddAllIds();
        query.AddLanguage(language);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/itemstats", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value =
                response.Json.RootElement.GetSet(static entry => entry.GetAttributeCombination());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves an attribute combination by its ID.</summary>
    /// <param name="attributeCombinationId">The attribute combination ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(AttributeCombination Value, MessageContext Context)>
        GetAttributeCombinationById(
            int attributeCombinationId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var query = new QueryBuilder();
        query.AddId(attributeCombinationId);
        query.AddLanguage(language);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/itemstats", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetAttributeCombination();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves attribute combinations by their IDs.</summary>
    /// <param name="attributeCombinationIds">The attribute combination IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<AttributeCombination> Value, MessageContext Context)>
        GetAttributeCombinationsByIds(
            IEnumerable<int> attributeCombinationIds,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var query = new QueryBuilder();
        query.AddIds(attributeCombinationIds);
        query.AddLanguage(language);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/itemstats", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value =
                response.Json.RootElement.GetSet(static entry => entry.GetAttributeCombination());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of attribute combinations.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<AttributeCombination> Value, MessageContext Context)>
        GetAttributeCombinationByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var query = new QueryBuilder();
        query.AddPage(pageIndex, pageSize);
        query.AddLanguage(language);
        query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = Request.HttpGet("v2/itemstats", query, null);
        var response = await Response.Json(httpClient, request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value =
                response.Json.RootElement.GetSet(static entry => entry.GetAttributeCombination());
            return (value, response.Context);
        }
    }

    #endregion v2/itemstats
}
