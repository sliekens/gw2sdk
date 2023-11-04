using GuildWars2.Gliders.Http;

namespace GuildWars2.Gliders;

[PublicAPI]
public sealed class GlidersQuery
{
    private readonly HttpClient http;

    public GlidersQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetGlidersIndex(
        CancellationToken cancellationToken = default
    )
    {
        GlidersIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
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
        return request.SendAsync(http, cancellationToken);
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
        return request.SendAsync(http, cancellationToken);
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
        return request.SendAsync(http, cancellationToken);
    }
}
