using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Skills.Http;
using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    public sealed class SkillService
    {
        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly ISkillReader skillReader;

        public SkillService(
            HttpClient http,
            ISkillReader skillReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.skillReader = skillReader ?? throw new ArgumentNullException(nameof(skillReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Skill>> GetSkills(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkillsRequest(language);
            return await http.GetResourcesSet(request, json => skillReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetSkillsIndex(CancellationToken cancellationToken = default)
        {
            var request = new SkillsIndexRequest();
            return await http.GetResourcesSet(request, json => skillReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Skill>> GetSkillById(
            int skillId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkillByIdRequest(skillId, language);
            return await http.GetResource(request, json => skillReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Skill>> GetSkillsByIds(
            IReadOnlyCollection<int> skillIds,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkillsByIdsRequest(skillIds, language);
            return await http.GetResourcesSet(request, json => skillReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Skill>> GetSkillsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkillsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request, json => skillReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
