using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Novelties;

[PublicAPI]
public sealed class NoveltyByIdRequest : IHttpRequest<IReplica<Novelty>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/novelties") { AcceptEncoding = "gzip" };

    public NoveltyByIdRequest(int noveltyId)
    {
        NoveltyId = noveltyId;
    }

    public int NoveltyId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Novelty>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", NoveltyId },
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

        var value = json.RootElement.GetNovelty(MissingMemberBehavior);
        return new Replica<Novelty>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
