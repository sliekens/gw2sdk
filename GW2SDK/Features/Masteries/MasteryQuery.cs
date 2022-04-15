using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Masteries.Http;
using GW2SDK.Masteries.Json;
using GW2SDK.Masteries.Models;
using JetBrains.Annotations;

namespace GW2SDK.Masteries;

[PublicAPI]
public sealed class MasteryQuery
{
    private readonly HttpClient http;

    public MasteryQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<IReplicaSet<Mastery>> GetMasteries(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MasteriesRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => MasteryReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetMasteriesIndex(CancellationToken cancellationToken = default)
    {
        MasteriesIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<Mastery>> GetMasteryById(
        int masteryId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MasteryByIdRequest request = new(masteryId, language);
        return await http.GetResource(request,
                json => MasteryReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<Mastery>> GetMasteriesByIds(
#if NET
        IReadOnlySet<int> masteryIds,
#else
        IReadOnlyCollection<int> masteryIds,
#endif
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MasteriesByIdsRequest request = new(masteryIds, language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => MasteryReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
