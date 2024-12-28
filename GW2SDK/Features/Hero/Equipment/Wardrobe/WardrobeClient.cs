using System.Runtime.CompilerServices;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Provides query methods for armor and weapon skins and skins unlocked on the account.</summary>
[PublicAPI]
public sealed class WardrobeClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="WardrobeClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public WardrobeClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/skins

    /// <summary>Retrieves the IDs of skins unlocked on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedSkins(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/skins", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetInt32());
            return (value, response.Context);
        }
    }

    #endregion v2/account/skins

    #region v2/skins

    /// <summary>Retrieves the IDs of all skins.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetSkinsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/skins");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a skin by its ID.</summary>
    /// <param name="skinId">The skin ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(EquipmentSkin Value, MessageContext Context)> GetSkinById(
        int skinId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/skins");
        requestBuilder.Query.AddId(skinId);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetEquipmentSkin();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves skins by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="skinIds">The skin IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<EquipmentSkin> Value, MessageContext Context)> GetSkinsByIds(
        IEnumerable<int> skinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/skins");
        requestBuilder.Query.AddIds(skinIds);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetEquipmentSkin());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of skins.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<EquipmentSkin> Value, MessageContext Context)> GetSkinsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/skins");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetEquipmentSkin());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves skins by their IDs by chunking requests and executing them in parallel. Supports more than 200 IDs.</summary>
    /// <param name="skinIds">The skin IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public IAsyncEnumerable<(EquipmentSkin Value, MessageContext Context)> GetSkinsBulk(
        IEnumerable<int> skinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            skinIds,
            GetChunk,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );

        // ReSharper disable once VariableHidesOuterVariable (intended, believe it or not)
        async Task<IReadOnlyCollection<(EquipmentSkin, MessageContext)>> GetChunk(
            IEnumerable<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var (values, context) = await GetSkinsByIds(
                    chunk,
                    language,
                    missingMemberBehavior,
                    cancellationToken
                )
                .ConfigureAwait(false);
            return values.Select(value => (value, context)).ToList();
        }
    }

    /// <summary>Retrieves all skins by chunking requests and executing them in parallel.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public async IAsyncEnumerable<(EquipmentSkin Value, MessageContext Context)> GetSkinsBulk(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var (value, _) = await GetSkinsIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetSkinsBulk(
            value,
            language,
            missingMemberBehavior,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach (var skin in producer.ConfigureAwait(false))
        {
            yield return skin;
        }
    }

    #endregion v2/skins
}
