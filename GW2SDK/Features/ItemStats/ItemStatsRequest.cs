using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats;

[PublicAPI]
public sealed class ItemStatsRequest : IHttpRequest<IReplicaSet<ItemStat>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/itemstats")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "ids", "all" } }
    };

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<ItemStat>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var request = Template with { AcceptLanguage = Language?.Alpha2Code };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetItemStat(MissingMemberBehavior));
        return new ReplicaSet<ItemStat>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
