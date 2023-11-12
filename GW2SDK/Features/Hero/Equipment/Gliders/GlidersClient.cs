using GuildWars2.Hero.Equipment.Gliders.Http;

namespace GuildWars2.Hero.Equipment.Gliders;

/// <summary>Provides query methods for glider skins and skins unlocked on the account.</summary>
[PublicAPI]
public sealed class GlidersClient
{
    private readonly HttpClient httpClient;

    public GlidersClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<Glider> Value, MessageContext Context)> GetGliders(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GlidersRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetGlidersIndex(
        CancellationToken cancellationToken = default
    )
    {
        GlidersIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Glider Value, MessageContext Context)> GetGliderById(
        int gliderId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GliderByIdRequest request = new(gliderId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Glider> Value, MessageContext Context)> GetGlidersByIds(
        IReadOnlyCollection<int> gliderIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GlidersByIdsRequest request = new(gliderIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Glider> Value, MessageContext Context)> GetGlidersByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GlidersByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
