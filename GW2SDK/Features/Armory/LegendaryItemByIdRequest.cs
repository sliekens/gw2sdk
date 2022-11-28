using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Armory;

[PublicAPI]
public sealed class LegendaryItemByIdRequest : IHttpRequest<IReplica<LegendaryItem>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/legendaryarmory")
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
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", LegendaryItemId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
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
