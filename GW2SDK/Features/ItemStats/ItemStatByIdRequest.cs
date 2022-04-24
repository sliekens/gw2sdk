using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats;

[PublicAPI]
public sealed class ItemStatByIdRequest : IHttpRequest<IReplica<ItemStat>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public ItemStatByIdRequest(int itemStatId)
    {
        ItemStatId = itemStatId;
    }

    public int ItemStatId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<ItemStat>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", ItemStatId);
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = ItemStatReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<ItemStat>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
