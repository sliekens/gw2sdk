using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Specializations.Http;
using GW2SDK.Specializations.Models;
using JetBrains.Annotations;

namespace GW2SDK.Specializations;

[PublicAPI]
public sealed class SpecializationQuery
{
    private readonly HttpClient http;

    public SpecializationQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public Task<IReplicaSet<Specialization>> GetSpecializations(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SpecializationsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetSpecializationsIndex(
        CancellationToken cancellationToken = default
    )
    {
        SpecializationsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Specialization>> GetSpecializationById(
        int specializationId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SpecializationByIdRequest request = new(specializationId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Specialization>> GetSpecializationsByIds(
        IReadOnlyCollection<int> specializationIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SpecializationsByIdsRequest request = new(specializationIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
