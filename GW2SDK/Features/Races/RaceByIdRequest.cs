using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Races;

[PublicAPI]
public sealed class RaceByIdRequest : IHttpRequest<IReplica<Race>>
{
    private static readonly HttpRequestMessageTemplate Template = new(HttpMethod.Get, "/v2/races")
    {
        AcceptEncoding = "gzip"
    };

    public RaceByIdRequest(RaceName raceId)
    {
        RaceId = raceId;
    }

    public RaceName RaceId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Race>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder { { "id", RaceId.ToString() } },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetRace(MissingMemberBehavior);
        return new Replica<Race>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
