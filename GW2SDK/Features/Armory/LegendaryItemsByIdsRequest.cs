using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Armory;

[PublicAPI]
public sealed class LegendaryItemsByIdsRequest : IHttpRequest<IReplicaSet<LegendaryItem>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/legendaryarmory")
    {
        AcceptEncoding = "gzip"
    };

    public LegendaryItemsByIdsRequest(IReadOnlyCollection<int> legendaryItemIds)
    {
        Check.Collection(legendaryItemIds, nameof(legendaryItemIds));
        LegendaryItemIds = legendaryItemIds;
    }

    public IReadOnlyCollection<int> LegendaryItemIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<LegendaryItem>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { Arguments = new QueryBuilder { { "ids", LegendaryItemIds } } },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetLegendaryItem(MissingMemberBehavior));
        return new ReplicaSet<LegendaryItem>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
