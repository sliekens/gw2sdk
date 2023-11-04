using System.Globalization;
using GuildWars2.Exploration.Floors;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Http;

internal sealed class FloorsByIdsRequest : IHttpRequest2<HashSet<Floor>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/continents/:id/floors") { AcceptEncoding = "gzip" };

    public FloorsByIdsRequest(int continentId, IReadOnlyCollection<int> floorIds)
    {
        Check.Collection(floorIds);
        ContinentId = continentId;
        FloorIds = floorIds;
    }

    public int ContinentId { get; }

    public IReadOnlyCollection<int> FloorIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Floor> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", ContinentId.ToString(CultureInfo.InvariantCulture)),
                    Arguments = new QueryBuilder
                    {
                        { "ids", FloorIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetFloor(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
