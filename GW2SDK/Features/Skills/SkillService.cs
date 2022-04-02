using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Skills.Http;
using GW2SDK.Skills.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    public sealed class SkillService
    {
        private readonly HttpClient http;

        public SkillService(HttpClient http)
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<Skill>> GetSkills(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkillsRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => SkillReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetSkillsIndex(CancellationToken cancellationToken = default)
        {
            var request = new SkillsIndexRequest();
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
            var request = new SkillByIdRequest(skillId, language);
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
            var request = new SkillsByIdsRequest(skillIds, language);
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
            var request = new SkillsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => SkillReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
