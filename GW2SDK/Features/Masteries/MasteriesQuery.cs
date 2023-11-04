using GuildWars2.Masteries.Http;

namespace GuildWars2.Masteries;

[PublicAPI]
public sealed class MasteriesQuery
{
    private readonly HttpClient http;

    public MasteriesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/masteries

    public Task<(HashSet<MasteryProgress> Value, MessageContext Context)> GetMasteryProgress(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        MasteryProgressRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/masteries

    #region v2/account/mastery/points

    public Task<(MasteryPointsProgress Value, MessageContext Context)> GetMasteryPointsProgress(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        MasteryPointsProgressRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/mastery/points

    #region v2/masteries

    public Task<(HashSet<Mastery> Value, MessageContext Context)> GetMasteries(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MasteriesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetMasteriesIndex(
        CancellationToken cancellationToken = default
    )
    {
        MasteriesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Mastery Value, MessageContext Context)> GetMasteryById(
        int masteryId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MasteryByIdRequest request = new(masteryId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Mastery> Value, MessageContext Context)> GetMasteriesByIds(
        IReadOnlyCollection<int> masteryIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MasteriesByIdsRequest request = new(masteryIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/masteries
}
