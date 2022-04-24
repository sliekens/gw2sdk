using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Prices;

[PublicAPI]
public sealed class ItemPricesByIdsRequest : IHttpRequest<IReplicaSet<ItemPrice>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/prices")
    {
        AcceptEncoding = "gzip"
    };

    public ItemPricesByIdsRequest(IReadOnlyCollection<int> itemIds)
    {
        Check.Collection(itemIds, nameof(itemIds));
        ItemIds = itemIds;
    }

    public IReadOnlyCollection<int> ItemIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<ItemPrice>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("ids", ItemIds);
        var request = Template with { Arguments = search };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value =
            json.RootElement.GetSet(entry => ItemPriceReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<ItemPrice>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
