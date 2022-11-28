using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Exploration.Regions;

[PublicAPI]
public sealed class RegionByIdRequest : IHttpRequest<IReplica<Region>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/continents/:id/floors/:floor/regions") { AcceptEncoding = "gzip" };

    public RegionByIdRequest(int continentId, int floorId, int regionId)
    {
        ContinentId = continentId;
        FloorId = floorId;
        RegionId = regionId;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public int RegionId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Region>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path
                        .Replace(":id", ContinentId.ToString(CultureInfo.InvariantCulture))
                        .Replace(":floor", FloorId.ToString(CultureInfo.InvariantCulture)),
                    Arguments = new QueryBuilder
                    {
                        { "id", RegionId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetRegion(MissingMemberBehavior);
        return new Replica<Region>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
