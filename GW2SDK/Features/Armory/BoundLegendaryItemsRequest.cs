using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Armory;

[PublicAPI]
public sealed class BoundLegendaryItemsRequest : IHttpRequest<IReplicaSet<BoundLegendaryItem>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/account/legendaryarmory")
        {
            Arguments = new QueryBuilder
            {
                { "ids", "all" },
                { "v", SchemaVersion.Recommended }
            }
        };

    public BoundLegendaryItemsRequest(string? accessToken)
    {
        AccessToken = accessToken;
    }

    public string? AccessToken { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<BoundLegendaryItem>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value =
            json.RootElement.GetSet(entry => entry.GetBoundLegendaryItem(MissingMemberBehavior));

        return new ReplicaSet<BoundLegendaryItem>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
