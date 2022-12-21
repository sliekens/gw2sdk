using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Specializations;

[PublicAPI]
public sealed class SpecializationsByIdsRequest : IHttpRequest<IReplicaSet<Specialization>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/specializations")
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
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        return new ReplicaSet<Specialization>
        {
            Values = json.RootElement.GetSet(
                entry => entry.GetSpecialization(MissingMemberBehavior)
            ),
            Context = response.Headers.GetCollectionContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
