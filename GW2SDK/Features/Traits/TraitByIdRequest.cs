using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits;

[PublicAPI]
public sealed class TraitByIdRequest : IHttpRequest<IReplica<Trait>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/traits")
    {
        AcceptEncoding = "gzip"
    };

    public TraitByIdRequest(int traitId)
    {
        TraitId = traitId;
    }

    public int TraitId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Trait>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder { { "id", TraitId }, { "v", SchemaVersion.Recommended } },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetTrait(MissingMemberBehavior);
        return new Replica<Trait>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
