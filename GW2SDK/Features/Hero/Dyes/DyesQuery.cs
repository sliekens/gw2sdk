using GuildWars2.Hero.Dyes.Http;

namespace GuildWars2.Hero.Dyes;

[PublicAPI]
public sealed class DyesQuery
{
    private readonly HttpClient http;

    public DyesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/dyes

    /// <summary>Gets the IDs of the dyes unlocked by the current account.</summary>
    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedDyesIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedDyesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/colors

    public Task<(HashSet<Dye> Value, MessageContext Context)> GetColors(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetColorsIndex(CancellationToken cancellationToken = default)
    {
        ColorsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Dye Value, MessageContext Context)> GetColorById(
        int colorId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorByIdRequest request = new(colorId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Dye> Value, MessageContext Context)> GetColorsByIds(
        IReadOnlyCollection<int> colorIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorsByIdsRequest request = new(colorIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Dye> Value, MessageContext Context)> GetColorsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
