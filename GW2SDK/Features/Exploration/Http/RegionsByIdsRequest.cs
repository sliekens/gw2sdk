using System.Globalization;
using GuildWars2.Exploration.Regions;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Http;

internal sealed class RegionsByIdsRequest : IHttpRequest<HashSet<Region>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/continents/:id/floors/:floor/regions") { AcceptEncoding = "gzip" };

    public RegionsByIdsRequest(int continentId, int floorId, IReadOnlyCollection<int> regionIds)
    {
        Check.Collection(regionIds);
        ContinentId = continentId;
        FloorId = floorId;
        RegionIds = regionIds;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public IReadOnlyCollection<int> RegionIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Region> Value, MessageContext Context)> SendAsync(
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
                        { "ids", RegionIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetRegion(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
