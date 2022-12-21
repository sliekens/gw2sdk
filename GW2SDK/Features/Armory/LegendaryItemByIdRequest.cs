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
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        return new Replica<LegendaryItem>
        {
            Value = json.RootElement.GetLegendaryItem(MissingMemberBehavior),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
