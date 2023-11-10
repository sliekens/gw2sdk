using System.Runtime.CompilerServices;
using GuildWars2.Hero.Equipment.Wardrobe.Http;

namespace GuildWars2.Hero.Equipment.Wardrobe;

[PublicAPI]
public sealed class WardrobeQuery
{
    private readonly HttpClient http;

    public WardrobeQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/skins

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedSkinsIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedSkinsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/skins

    #region v2/skins

    public Task<(HashSet<int> Value, MessageContext Context)> GetSkinsIndex(CancellationToken cancellationToken = default)
    {
        SkinsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Skin Value, MessageContext Context)> GetSkinById(
        int skinId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkinByIdRequest request = new(skinId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Skin> Value, MessageContext Context)> GetSkinsByIds(
        IReadOnlyCollection<int> skinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkinsByIdsRequest request = new(skinIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Skin> Value, MessageContext Context)> GetSkinsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkinsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public IAsyncEnumerable<(Skin Value, MessageContext Context)> GetSkinsBulk(
        IReadOnlyCollection<int> skinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
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
        async Task<IReadOnlyCollection<(Skin, MessageContext) >> GetChunk(
            IReadOnlyCollection<int> chunk,
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

    public async IAsyncEnumerable<(Skin Value, MessageContext Context)> GetSkinsBulk(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
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
