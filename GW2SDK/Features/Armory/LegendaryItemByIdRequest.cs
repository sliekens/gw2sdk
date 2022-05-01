using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Armory;

[PublicAPI]
public sealed class LegendaryItemByIdRequest : IHttpRequest<IReplica<LegendaryItem>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/legendaryarmory")
    {
        AcceptEncoding = "gzip"
    };

    public LegendaryItemByIdRequest(int legendaryItemId)
    {
        LegendaryItemId = legendaryItemId;
    }

    public int LegendaryItemId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<LegendaryItem>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", LegendaryItemId);
        var request = Template with
        {
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

        var value = json.RootElement.GetLegendaryItem(MissingMemberBehavior);
        return new Replica<LegendaryItem>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
