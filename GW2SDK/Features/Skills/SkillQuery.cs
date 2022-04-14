using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Skills.Http;
using GW2SDK.Skills.Json;
using GW2SDK.Skills.Models;

using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed class SkillQuery
{
    private readonly HttpClient http;

    public SkillQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<IReplicaSet<Skill>> GetSkills(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkillsRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => SkillReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetSkillsIndex(CancellationToken cancellationToken = default)
    {
        SkillsIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<Skill>> GetSkillById(
        int skillId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkillByIdRequest request = new(skillId, language);
        return await http.GetResource(request,
                json => SkillReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<Skill>> GetSkillsByIds(
#if NET
        IReadOnlySet<int> skillIds,
#else
        IReadOnlyCollection<int> skillIds,
#endif
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkillsByIdsRequest request = new(skillIds, language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => SkillReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaPage<Skill>> GetSkillsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkillsByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => SkillReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }
}