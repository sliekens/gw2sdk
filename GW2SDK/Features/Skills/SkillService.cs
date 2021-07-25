using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        private readonly ISkillReader _skillReader;

        public SkillService(
            HttpClient http,
            ISkillReader skillReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _skillReader = skillReader ?? throw new ArgumentNullException(nameof(skillReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Skill>> GetSkills(Language? language = default)
        {
            var request = new SkillsRequest(language);
            return await _http.GetResourcesSet(request, json => _skillReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetSkillsIndex()
        {
            var request = new SkillsIndexRequest();
            return await _http.GetResourcesSet(request, json => _skillReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Skill>> GetSkillById(int skillId, Language? language = default)
        {
            var request = new SkillByIdRequest(skillId, language);
            return await _http.GetResource(request, json => _skillReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Skill>> GetSkillsByIds(
            IReadOnlyCollection<int> skillIds,
            Language? language = default
        )
        {
            var request = new SkillsByIdsRequest(skillIds, language);
            return await _http.GetResourcesSet(request, json => _skillReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Skill>> GetSkillsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new SkillsByPageRequest(pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request, json => _skillReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
