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
public sealed class FloorByIdRequest : IHttpRequest<IReplica<Floor>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/continents/:id/floors") { AcceptEncoding = "gzip" };

    public FloorByIdRequest(int continentId, int floorId)
    {
        ContinentId = continentId;
        FloorId = floorId;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Floor>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", FloorId);
        var request = Template with
        {
            Path = Template.Path.Replace(
                ":id",
                ContinentId.ToString(CultureInfo.InvariantCulture)
                ),
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

        var value = json.RootElement.GetFloor(MissingMemberBehavior);
        return new Replica<Floor>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
