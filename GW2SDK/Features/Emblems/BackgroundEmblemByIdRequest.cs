using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Emblems;

[PublicAPI]
public sealed class BackgroundEmblemByIdRequest : IHttpRequest<IReplica<Emblem>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/emblem/backgrounds") { AcceptEncoding = "gzip" };

    public BackgroundEmblemByIdRequest(int backgroundEmblemId)
    {
        BackgroundEmblemId = backgroundEmblemId;
    }

    public int BackgroundEmblemId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Emblem>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", BackgroundEmblemId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetEmblem(MissingMemberBehavior);
        return new Replica<Emblem>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
