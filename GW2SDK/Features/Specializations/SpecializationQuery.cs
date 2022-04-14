using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Specializations.Http;
using GW2SDK.Specializations.Json;
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

    public async Task<IReplicaSet<Specialization>> GetSpecializations(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SpecializationsRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => SpecializationReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetSpecializationsIndex(CancellationToken cancellationToken = default)
    {
        SpecializationsIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<Specialization>> GetSpecializationById(
        int specializationId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SpecializationByIdRequest request = new(specializationId, language);
        return await http.GetResource(request,
                json => SpecializationReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<Specialization>> GetSpecializationsByIds(
#if NET
        IReadOnlySet<int> specializationIds,
#else
        IReadOnlyCollection<int> specializationIds,
#endif
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SpecializationsByIdsRequest request = new(specializationIds, language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => SpecializationReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }
}