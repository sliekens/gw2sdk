using GuildWars2.Hero.Equipment.Finishers.Http;

namespace GuildWars2.Hero.Equipment.Finishers;

/// <summary>Provides query methods for enemy finishers and unlocked finishers on the account.</summary>
[PublicAPI]
public sealed class FinishersClient
{
    private readonly HttpClient httpClient;

    public FinishersClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/finishers

    public Task<(HashSet<UnlockedFinisher> Value, MessageContext Context)> GetUnlockedFinishers(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedFinishersRequest
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/finishers

    public Task<(HashSet<Finisher> Value, MessageContext Context)> GetFinishers(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FinishersRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetFinishersIndex(
        CancellationToken cancellationToken = default
    )
    {
        FinishersIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Finisher Value, MessageContext Context)> GetFinisherById(
        int finisherId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FinisherByIdRequest request = new(finisherId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Finisher> Value, MessageContext Context)> GetFinishersByIds(
        IReadOnlyCollection<int> finisherIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FinishersByIdsRequest request = new(finisherIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Finisher> Value, MessageContext Context)> GetFinishersByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FinishersByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
