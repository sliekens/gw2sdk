using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps.Http;

[PublicAPI]
public sealed class FloorsIndexRequest : IHttpRequest<IReplicaSet<int>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/continents/:id/floors") { AcceptEncoding = "gzip" };

    public FloorsIndexRequest(int continentId)
    {
        ContinentId = continentId;
    }

    public int ContinentId { get; }

    public async Task<IReplicaSet<int>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var request = Template with
        {
            Path = Template.Path.Replace(
                ":id",
                ContinentId.ToString(CultureInfo.InvariantCulture)
                )
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

        var value = json.RootElement.GetSet(entry => entry.GetInt32());
        return new ReplicaSet<int>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
