using GuildWars2.Legends.Http;

namespace GuildWars2.Legends;

[PublicAPI]
public sealed class LegendsClient
{
    private readonly HttpClient httpClient;

    public LegendsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetLegendsIndex(
        CancellationToken cancellationToken = default
    )
    {
        LegendsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Legend Value, MessageContext Context)> GetLegendById(
        string legendId,
        CancellationToken cancellationToken = default
    )
    {
        LegendByIdRequest request = new(legendId)
        {
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Legend> Value, MessageContext Context)> GetLegendsByIds(
        IReadOnlyCollection<string> legendIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendsByIdsRequest request = new(legendIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Legend> Value, MessageContext Context)> GetLegendsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Legend> Value, MessageContext Context)> GetLegends(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
