using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Specializations;

[PublicAPI]
public sealed class SpecializationsByIdsRequest : IHttpRequest<IReplicaSet<Specialization>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/specializations")
    {
        AcceptEncoding = "gzip"
    };

    public SpecializationsByIdsRequest(IReadOnlyCollection<int> specializationIds)
    {
        Check.Collection(specializationIds, nameof(specializationIds));
        SpecializationIds = specializationIds;
    }

    public IReadOnlyCollection<int> SpecializationIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Specialization>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", SpecializationIds },
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

        var value = json.RootElement.GetSet(
            entry => entry.GetSpecialization(MissingMemberBehavior)
        );
        return new ReplicaSet<Specialization>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
