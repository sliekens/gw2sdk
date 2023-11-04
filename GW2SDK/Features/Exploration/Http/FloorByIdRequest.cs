using System.Globalization;
using GuildWars2.Exploration.Floors;
using GuildWars2.Http;

namespace GuildWars2.Exploration.Http;

internal sealed class FloorByIdRequest : IHttpRequest2<Floor>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/continents/:id/floors") { AcceptEncoding = "gzip" };

    public FloorByIdRequest(int continentId, int floorId)
    {
        ContinentId = continentId;
        FloorId = floorId;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Floor Value, MessageContext Context)> SendAsync(
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
                        { "id", FloorId },
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
        var value = json.RootElement.GetFloor(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
