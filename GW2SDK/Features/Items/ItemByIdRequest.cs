using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Items;

[PublicAPI]
public sealed class ItemByIdRequest : IHttpRequest<IReplica<Item>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/items")
    {
        AcceptEncoding = "gzip"
    };

    public ItemByIdRequest(int itemId)
    {
        ItemId = itemId;
    }

    public int ItemId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; set; }

    public async Task<IReplica<Item>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", ItemId);
        var request = Template with
        {
            AcceptLanguage = Language?.Alpha2Code,
            Arguments = search
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

        return new Replica<Item>(
            response.Headers.Date.GetValueOrDefault(),
            json.RootElement.GetItem(MissingMemberBehavior),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
