using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps;

[PublicAPI]
public sealed class FloorsRequest : IHttpRequest<IReplicaSet<Floor>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/continents/:id/floors")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "ids", "all" } }
        };

    public FloorsRequest(int continentId)
    {
        ContinentId = continentId;
    }

    public int ContinentId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Floor>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var request = Template with
        {
            Path = Template.Path.Replace(
                ":id",
                ContinentId.ToString(CultureInfo.InvariantCulture)
            ),
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

        var value = json.RootElement.GetSet(entry => entry.GetFloor(MissingMemberBehavior));
        return new ReplicaSet<Floor>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
